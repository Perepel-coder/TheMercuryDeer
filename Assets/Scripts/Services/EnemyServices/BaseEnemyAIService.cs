using Assets.Scripts.Application.Interfaces.Entity;
using Assets.Scripts.Application.Interfaces.NpcStates;
using Assets.Scripts.Application.Interfaces.Weapon;
using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.Enemies.StateHandler;
using Assets.Scripts.Services.Player;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static Assets.Scripts.Enums.EnemyEnums.EnemyDefinitions;

public abstract partial class BaseEnemyAIService
{
    protected State _currentState = State.Roaming;
    private const float JUMP_POWER = 0.5f;

    private float _roamingCurrentTime;
    private Vector3 _lastSteeringTarget;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    protected BaseEntityService _ownerEntity;
    protected abstract EnemyTag Name { get; }

    public EnemyDTO Stats { get; protected set; }

    public float MaxHealth => Stats.MaxHealth;
    public float CurrentHealth { get; set; }

    #region state heandlers
    protected IRoamingStateHandler<BaseEnemyAIService> _roamingStateHandler = new BaseRoamingStateHandler();
    protected IChasingStateHandler<BaseEnemyAIService> _chasingStateHandler = new BaseChasingStateHandler();
    protected IAttackingStateHandler<BaseEnemyAIService> _attackingStateHandler = new BaseAttackingStateHandler();
    #endregion

    #region inventory
    public ActiveWeaponService ActiveWeapon { get; protected set; }
    public ActiveWeaponService ReactionToTakingHit { get; protected set; }
    #endregion
}

public abstract partial class BaseEnemyAIService : MonoBehaviour, IHasState, IHasHealth
{
    public bool IsRunning => _navMeshAgent.velocity != Vector3.zero;
    public event EventHandler OnEnemyAttacked;
    public Vector3 CurrentPoison => transform.position;
    public Vector3 GetTopTransformPosition => new(transform.position.x, _collider.bounds.max.y, transform.position.z);

    public void StateHandler()
    {
        switch (_currentState)
        {
            case State.Roaming:
                _roamingCurrentTime -= Time.deltaTime;
                if (_roamingCurrentTime < 0f)
                {
                    _roamingCurrentTime = Stats.RoamingTimeMax;
                    _roamingStateHandler.Run(this);
                    _navMeshAgent.SetDestination(_roamingStateHandler.TargetPosition);
                }
                break;
            case State.Chasing:
                _chasingStateHandler.Run(this);
                _navMeshAgent.SetDestination(_chasingStateHandler.TargetPosition);
                break;
            case State.Attacking:
                if (Time.time > Stats.NextAttackTime)
                {
                    OnEnemyAttacked?.Invoke(this, EventArgs.Empty);
                    _attackingStateHandler.Run(this);
                    Stats.NextAttackTime = _attackingStateHandler.NextAttackTime;
                }
                break;
        }

        CheckCurrentState();
        TrackingDirectionMovement();
    }

    protected virtual bool CheckAttackingState(float distanceToPlayer) => distanceToPlayer <= Stats.AttackingDistance;

    protected virtual bool CheckChasingState(float distanceToPlayer) => distanceToPlayer <= Stats.ChasingDistance;

    public void CheckCurrentState()
    {
        if (_currentState == State.Death) return;

        if (!PlayerEntityService.Instance.IsAlive)
        {
            _currentState = State.Roaming;
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, PlayerService.Instance.transform.position);

        State newState = !_ownerEntity.IsAlive ?
            State.Death :
            CheckAttackingState(distanceToPlayer) ?
            State.Attacking :
            CheckChasingState(distanceToPlayer) ?
            State.Chasing :
            State.Roaming;

        if (newState != _currentState)
        {
            switch (newState)
            {
                case State.Chasing:
                    _navMeshAgent.ResetPath();
                    _navMeshAgent.speed = Stats.RoamingSpeed * Stats.ChasingSpeedMultiplier;
                    break;
                case State.Roaming:
                    _roamingCurrentTime = 0f;
                    _navMeshAgent.speed = Stats.RoamingSpeed;
                    break;
                case State.Attacking:
                    _navMeshAgent.ResetPath();
                    break;
                case State.Death:
                    _navMeshAgent.ResetPath();
                    _navMeshAgent.enabled = false;
                    _collider.enabled = false;
                    foreach (var c in GetComponentsInChildren<Collider2D>())
                        c.enabled = false;
                    break;
            }

            _currentState = newState;
        }
    }

    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();

        Stats = DatabaseService.EnemyRepository.GetEnemy(Name);

        _navMeshAgent.speed = Stats.RoamingSpeed;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    protected virtual void Start()
    {
        _ownerEntity = GetComponent<BaseEntityService>();

        var weapons = GetComponentsInChildren<ActiveWeaponService>();

        ActiveWeapon = weapons.SingleOrDefault(w => w.Weapon is IMainWeapon);
        ReactionToTakingHit = weapons.SingleOrDefault(w => w.Weapon is IDamageReaction);
    }

    private void Update() => StateHandler();

    protected virtual void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition) =>
        transform.rotation = currentPosition.x < targetPosition.x ?
        Quaternion.Euler(0, 180, 0) :
        Quaternion.Euler(0, 0, 0);

    private void TrackingDirectionMovement()
    {
        if (_lastSteeringTarget == _navMeshAgent.steeringTarget)
            return;

        switch (_currentState)
        {
            case State.Roaming or State.Chasing:
                ChangeFacingDirection(transform.position, _navMeshAgent.steeringTarget);
                break;
            case State.Attacking:
                ChangeFacingDirection(transform.position, PlayerService.Instance.transform.position);
                break;
        }

        _lastSteeringTarget = _navMeshAgent.steeringTarget;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.IsTouching(_collider)) return;

        if (collision.transform.TryGetComponent(out IDamageable _))
        {
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            _rigidbody.MovePosition((_rigidbody.position + direction * JUMP_POWER) / _rigidbody.mass);

            _navMeshAgent.ResetPath();
        }
    }
}
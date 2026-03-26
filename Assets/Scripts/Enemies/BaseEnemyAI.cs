using Assets.Scripts.Enemies.StateHandler;
using Assets.Scripts.Interfaces.IStateHandler;
using Assets.Scripts.Interfaces.Npc;
using Assets.Scripts.Interfaces.Weapon;
using System;
using System.Linq;
using TheMercuryDeer.Scripts.Enemy;
using UnityEngine;
using UnityEngine.AI;

public abstract partial class BaseEnemyAI
{
    [SerializeField] protected State _currentState = State.Idle;

    private float _roamingCurrentTime;
    private Vector3 _lastSteeringTarget;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private const float JUMP_DISTANCE = 0.5f;

    #region state heandlers
    protected IRoamingStateHandler<BaseEnemyAI> _roamingStateHandler = new BaseRoamingStateHandler();
    protected IChasingStateHandler<BaseEnemyAI> _chasingStateHandler = new BaseChasingStateHandler();
    protected IAttackingStateHandler<BaseEnemyAI> _attackingStateHandler = new BaseAttackingStateHandler();
    #endregion

    #region characteristics
    public float RoamingDistanceMin { get; protected set; } = 2f;
    public float RoamingDistanceMax { get; protected set; } = 6f;
    public float RoamingTimeMax { get; protected set; } = 4f;
    public float RoamingSpeed { get; protected set; } = 1f;

    public float ChasingDistance { get; protected set; } = 5f;
    public float ChasingSpeedMultiplier { get; protected set; } = 2f;

    public float AttackingDistance { get; protected set; } = 0.5f;
    public float AttackRate { get; protected set; } = 2f;
    public float NextAttackTime { get; protected set; } = 0f;
    public int InherentDamage { get; protected set; } = 1;
    #endregion

    #region inventory
    public ActiveWeapon? ActiveWeapon { get; protected set; }
    public ActiveWeapon? ReactionToTakingHit { get; protected set; }
    #endregion
}

public abstract partial class BaseEnemyAI : MonoBehaviour, IHasState
{
    public bool IsRunning => _navMeshAgent.velocity != Vector3.zero;
    public event EventHandler? OnEnemyAttacked;

    public Vector3 CurrentPoison => transform.position;

    public abstract int MaxHealth { get; }
    public abstract bool IsEnemy { get; }
    public abstract bool IsChasingEnemy { get; }

    public void StateHandler()
    {
        switch (_currentState)
        {
            case State.Roaming:
                _roamingCurrentTime -= Time.deltaTime;
                if (_roamingCurrentTime < 0f)
                {
                    _roamingCurrentTime = RoamingTimeMax;
                    _roamingStateHandler.Run(this);
                    _navMeshAgent.SetDestination(_roamingStateHandler.TargetPosition);
                }
                break;
            case State.Chasing:
                _chasingStateHandler.Run(this);
                _navMeshAgent.SetDestination(_chasingStateHandler.TargetPosition);
                break;
            case State.Attacking:
                if (Time.time > NextAttackTime)
                {
                    OnEnemyAttacked?.Invoke(this, EventArgs.Empty);
                    _attackingStateHandler.Run(this);
                    NextAttackTime = _attackingStateHandler.NextAttackTime;
                }
                break;
        }

        CheckCurrentState();
        TrackingDirectionMovement();
    }

    protected virtual bool CheckAttackingState(float distanceToPlayer) =>
        IsEnemy && distanceToPlayer <= AttackingDistance;

    protected virtual bool CheckChasingState(float distanceToPlayer) =>
        IsChasingEnemy && distanceToPlayer <= ChasingDistance;


    public void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

        State newState = 
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
                    _navMeshAgent.speed = RoamingSpeed * ChasingSpeedMultiplier;
                    break;
                case State.Roaming:
                    _roamingCurrentTime = 0f;
                    _navMeshAgent.speed = RoamingSpeed;
                    break;
                case State.Attacking:
                    _navMeshAgent.ResetPath();
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

        _navMeshAgent.speed = RoamingSpeed;
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    protected virtual void Start()
    {
        var weapons = GetComponentsInChildren<ActiveWeapon>();

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
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
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
            _rigidbody.MovePosition(_rigidbody.position + direction * JUMP_DISTANCE);

            _navMeshAgent.ResetPath();
        }
    }
}
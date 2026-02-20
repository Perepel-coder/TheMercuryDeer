using System;
using TheMercuryDeer.Scripts.Enemy;
using TheMercuryDeer.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;

public abstract class NpcAI : MonoBehaviour
{
    #region private
    private NavMeshAgent _navMeshAgent;
    private float _roamingCurrentTime;
    #endregion

    #region characteristics
    [SerializeField] protected State _currentState = State.Idle;

    [SerializeField] protected float _roamingDistanceMin = 3f;
    [SerializeField] protected float _roamingDistanceMax = 7f;
    [SerializeField] protected float _roamingTimeMax = 2f;
    [SerializeField] protected float _roamingSpeed = 1.0f;

    protected float _chasingDistance = 4f;
    protected float _chasingSpeedMultiplayer = 2f;

    protected float _attackingDistance = 2f;
    protected float _attackRate = 2f;
    protected float _nextAttackTime = 0f;
    #endregion

    protected float ChasingSpeed => _roamingSpeed * 2.0f;
    protected Vector3 _movementVector;

    public bool IsRunning => _navMeshAgent.velocity != Vector3.zero;
    public event EventHandler OnEnemyAttacked;

    public abstract int MaxHealth { get; }
    public abstract bool IsEnemy { get; }
    public abstract bool IsChasingEnemy { get; }


    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    protected virtual void Start()
    {
        _roamingSpeed = _navMeshAgent.speed;
    }

    private void Update() => StateHandler();

    private void StateHandler()
    {
        switch (_currentState)
        {
            case State.Roaming:
                _roamingCurrentTime -= Time.deltaTime;
                if (_roamingCurrentTime < 0f)
                    HandleRoamingState();
                break;
            case State.Chasing:
                HandleChasingState();
                break;
            case State.Attacking:
                HandleAttackingState();
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;
        }

        CheckCurrentState();
    }

    private void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition) =>
        transform.rotation = currentPosition.x < targetPosition.x ?
        Quaternion.Euler(0, 180, 0) :
        Quaternion.Euler(0, 0, 0);

    private void HandleRoamingState()
    {
        _roamingCurrentTime = _roamingTimeMax;

        var targetPosition = transform.position + Utils.GetRandomDirection() * UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);

        ChangeFacingDirection(transform.position, targetPosition);

        _navMeshAgent.SetDestination(targetPosition);
    }

    private void HandleChasingState() => _navMeshAgent.SetDestination(Player.Instance.transform.position);

    private void HandleAttackingState()
    {
        if (Time.time > _nextAttackTime)
        {
            OnEnemyAttacked.Invoke(this, EventArgs.Empty);

            _nextAttackTime = Time.time + _attackRate;
        }
    }

    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);

        State newState =
            IsChasingEnemy && distanceToPlayer <= _chasingDistance ? State.Chasing :
            IsEnemy && distanceToPlayer <= _attackingDistance ? State.Attacking : State.Roaming;

        if (newState != _currentState)
        {
            switch (newState)
            {
                case State.Chasing:
                    _navMeshAgent.ResetPath();
                    _navMeshAgent.speed = ChasingSpeed;
                    break;
                case State.Roaming:
                    _roamingCurrentTime = 0f;
                    _navMeshAgent.speed = _roamingSpeed;
                    break;
                case State.Attacking:
                    _navMeshAgent.ResetPath();
                    break;
            }

            _currentState = newState;
        }
    }
}
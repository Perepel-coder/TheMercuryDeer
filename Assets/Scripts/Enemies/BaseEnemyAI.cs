using System;
using TheMercuryDeer.Scripts.Enemy;
using TheMercuryDeer.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemyAI : MonoBehaviour
{
    #region private
    private NavMeshAgent _navMeshAgent;
    private float _roamingCurrentTime;
    private Vector3 _lastSteeringTarget;
    #endregion

    #region characteristics
    protected State _currentState = State.Idle;

    protected float _roamingDistanceMin = 4f;
    protected float _roamingDistanceMax = 6f;
    protected float _roamingTimeMax = 4f;
    protected float _roamingSpeed = 3.5f;

    protected float _chasingDistance = 15f;
    protected float _chasingSpeedMultiplier = 2f;

    protected float _attackingDistance = 7f;
    protected float _attackRate = 2f;
    protected float _nextAttackTime = 0f;
    #endregion

    public bool IsRunning => _navMeshAgent.velocity != Vector3.zero;
    public float ChasingSpeedMultiplier => _chasingSpeedMultiplier;
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

    protected virtual void Start() {}

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
        TrackingDirectionMovement();
    }

    private void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition) =>
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
                    _navMeshAgent.speed = _roamingSpeed * _chasingSpeedMultiplier;
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

    protected virtual void HandleRoamingState()
    {
        _roamingCurrentTime = _roamingTimeMax;

        var targetPosition = transform.position + Utils.GetRandomDirection() * UnityEngine.Random.Range(_roamingDistanceMin, _roamingDistanceMax);

        _navMeshAgent.SetDestination(targetPosition);
    }

    protected virtual void HandleChasingState() => _navMeshAgent.SetDestination(Player.Instance.transform.position);

    protected virtual void HandleAttackingState()
    {
        if (Time.time > _nextAttackTime)
        {
            OnEnemyAttacked.Invoke(this, EventArgs.Empty);

            _nextAttackTime = Time.time + _attackRate;
        }
    }
}
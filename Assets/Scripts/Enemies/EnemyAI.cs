using TheMercuryDeer.Scripts.Enemy;
using TheMercuryDeer.Scripts.Utils;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyAI : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private float _roamingCurrentTime;

    [SerializeField] protected State _state = State.Idle;

    [SerializeField] protected float _roamingDistanceMin = 3f;
    [SerializeField] protected float _roamingDistanceMax = 7f;
    [SerializeField] protected float _roamingTimerMax = 2f;
    [SerializeField] protected float _roamingSpeed = 1.0f;

    [SerializeField] protected float _chasingDistance = 4f;
    [SerializeField] protected float _chasingSpeedMultiplayer = 2f;

    protected float GetChasingSpeed => _roamingSpeed * 2.0f;
    protected Vector3 _movementVector;

    public bool IsRunning => _navMeshAgent.velocity != Vector3.zero;
    public abstract int MaxHealth { get; }
    public abstract bool IsChasingEnemy { get; }

    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    protected virtual void Start()
    {

    }

    private void Update() => StateHandler();

    private void StateHandler()
    {
        switch (_state)
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
                //HandleAttackingState();
                break;
            case State.Death:
                break;
            default:
            case State.Idle:
                break;
        }
    }

    private void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition) =>
        transform.rotation = currentPosition.x < targetPosition.x ? 
        Quaternion.Euler(0, 180, 0) : 
        Quaternion.Euler(0, 0, 0);

    private void HandleRoamingState()
    {
        _roamingCurrentTime = _roamingTimerMax;

        var targetPosition = transform.position + Utils.GetRandomDirection() * Random.Range(_roamingDistanceMin, _roamingDistanceMax);

        ChangeFacingDirection(transform.position, targetPosition);

        _navMeshAgent.SetDestination(targetPosition);
    }

    private void HandleChasingState()
    {

    }
}
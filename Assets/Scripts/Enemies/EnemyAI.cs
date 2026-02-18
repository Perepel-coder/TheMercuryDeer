using TheMercuryDeer.Scripts.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State _state = State.Idle;

    private Enemy _enemy;
    private NavMeshAgent _navMeshAgent;
    private float _roamingCurrentTime;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<Enemy>();

        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }

    private void Update()
    {
        switch (_state)
        {
            default:
            case State.Roaming:
                _roamingCurrentTime -= Time.deltaTime;
                if (_roamingCurrentTime < 0f)
                    HandleRoamingState();
                break;
            case State.Chasing:
                //HandleChasingState();
                break;
            case State.Attacking:
                //HandleAttackingState();
                break;
        }
    }

    private void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition) =>
        transform.rotation = currentPosition.x < targetPosition.x ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);

    private void HandleRoamingState()
    {
        _roamingCurrentTime = _enemy.RoamingTimerMax;
        _enemy.HandleRoamingState(out Vector3 targetPosition);

        ChangeFacingDirection(transform.position, targetPosition);

        _navMeshAgent.SetDestination(targetPosition);
    }
}

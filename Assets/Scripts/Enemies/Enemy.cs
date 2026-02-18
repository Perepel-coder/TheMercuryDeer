using UnityEngine;
using TheMercuryDeer.Scripts.Utils;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _roamingDistanceMin = 3f;
    [SerializeField] protected float _roamingDistanceMax = 7f;
    [SerializeField] protected float _roamingTimerMax = 2f;

    protected Vector3 movementVector;

    public float RoamingTimerMax => _roamingDistanceMax;
    public abstract int MaxHealth { get; protected set; }

    public virtual void HandleRoamingState(out Vector3 targetPosition)
    {
        movementVector = Utils.GetRandomDirection();
        targetPosition = transform.position + movementVector * Random.Range(_roamingDistanceMin, _roamingDistanceMax);
    }
}
using Assets.Scripts.Interfaces;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Collider2D _collider;

    public bool IsAttacking { get; protected set; }

    public abstract int DamageAmount { get; protected set; }

    public abstract void Attack();


    protected virtual void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    protected virtual void Start()
    {
        TurnOnCollider(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable enemy))
            enemy.TakeDamage(DamageAmount);
    }

    public void TurnOnCollider(bool enable)
    {
        _collider.enabled = enable;
        IsAttacking = enable;
    }
}
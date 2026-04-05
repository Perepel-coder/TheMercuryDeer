using Assets.Scripts.Application.Interfaces.Entity;
using Assets.Scripts.DTO;
using Assets.Scripts.Enums;
using Assets.Scripts.Infrastructure;
using UnityEngine;

public abstract class WeaponService : MonoBehaviour
{
    protected Collider2D _collider;

    protected abstract WeaponTag Tag { get; }

    public WeaponDTO Stats { get; protected set; }

    public bool IsAttacking { get; protected set; }

    public abstract void Attack();


    protected virtual void Awake()
    {
        _collider = GetComponent<Collider2D>();
        Stats = DatabaseService.WeaponRepository.GetWeaponByTag(Tag);
    }

    protected virtual void Start()
    {
        TurnOnCollider(false);
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable enemy) && Stats.IsContinuousDamage)
            enemy.TakeDamage(Stats.DamageAmount);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out IDamageable enemy) && !Stats.IsContinuousDamage)
            enemy.TakeDamage(Stats.DamageAmount);
    }

    public void TurnOnCollider(bool enable)
    {
        _collider.enabled = enable;
        IsAttacking = enable;
    }
}
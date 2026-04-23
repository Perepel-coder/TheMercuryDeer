using Assets.Scripts.Constants.Paths;
using Assets.Scripts.Interfaces.Entity;
using Assets.Scripts.ScriptableObjects;
using UnityEngine;
using static Assets.Scripts.Constants.ItemDefinitions;

public abstract class WeaponService : MonoBehaviour
{
    protected Collider2D _collider;

    protected abstract WeaponTag WeaponTag { get; }

    public WeaponDataSO Stats { get; protected set; }

    public bool IsAttacking { get; protected set; }

    public abstract void Attack();


    protected virtual void Awake()
    {
        _collider = GetComponent<Collider2D>();
        Stats = Resources.Load<WeaponDataSO>($"{ResourcePaths.ScriptableObjects.PATH_TO_WEAPONS}{WeaponTag}");
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
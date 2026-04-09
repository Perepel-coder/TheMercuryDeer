using Assets.Scripts.Application.Interfaces.Entity;
using System;
using UnityEngine;

public class BaseEntityService : MonoBehaviour, IDamageable, IHealable
{
    protected IHasHealth _ownerAI;

    public event EventHandler OnTakedDamage;
    public event EventHandler OnDeath;

    public bool IsAlive { get; private set; } = true;

    protected virtual void Awake() { }

    protected virtual void Start()
    {
        _ownerAI = GetComponent<IHasHealth>();

        _ownerAI.CurrentHealth = _ownerAI.MaxHealth;
    }

    protected virtual void DetectDeath()
    {
        if (_ownerAI.CurrentHealth <= 0) Die();
    }

    public virtual void TakeDamage(int damage, Vector3? enemyPosition = null)
    {
        OnTakedDamage?.Invoke(this, EventArgs.Empty);

        _ownerAI.CurrentHealth -= damage;

        DetectDeath();
    }

    public virtual void RestoreHealth(float health)
    {
        _ownerAI.CurrentHealth += health;

        if (_ownerAI.CurrentHealth > _ownerAI.MaxHealth)
            _ownerAI.CurrentHealth = _ownerAI.MaxHealth;
    }

    public virtual void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);

        IsAlive = false;
    }
}
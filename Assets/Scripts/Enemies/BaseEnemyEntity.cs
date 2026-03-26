using Assets.Scripts.Interfaces.Npc;
using Assets.Scripts.Interfaces.NpcEntity;
using System;
using UnityEngine;

public class BaseEnemyEntity : MonoBehaviour, IDamageable, IHealable
{
    private BaseEnemyAI _ownerAI;
    private int _currentHealth;

    public event EventHandler? OnTakedDamage;
    public event EventHandler? OnDeath;

    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        _ownerAI = GetComponent<BaseEnemyAI>();
    }

    private void Start()
    {
        _currentHealth = _ownerAI.MaxHealth;
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);

            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        OnTakedDamage?.Invoke(this, EventArgs.Empty);

        _currentHealth -= damage;

        _ownerAI.ReactionToTakingHit?.Weapon.Attack();

        DetectDeath();
    }

    public void RestoreHealth(int health)
    {
        _currentHealth += health;
    }

    public void Die()
    {
        //Destroy(gameObject);
        IsAlive = false;
    }
}
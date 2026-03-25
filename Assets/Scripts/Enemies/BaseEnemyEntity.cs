using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseEnemyEntity : MonoBehaviour, IDamageable
{
    private BaseEnemyAI _enemyAI;
    private int _currentHealth;

    //public event EventHandle OnEnemyTakedDamage;

    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        _enemyAI = GetComponent<BaseEnemyAI>();
    }

    private void Start()
    {
        _currentHealth = _enemyAI.MaxHealth;
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
            Die();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        DetectDeath();
    }

    public void Die()
    {
        Destroy(gameObject);
        IsAlive = false;
    }
}
using System;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    private EnemyAI _enemyAI;
    private int _maxHealth;
    private int _currentHealth;

    private void Awake()
    {
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void Start()
    {
        _maxHealth = _enemyAI.MaxHealth;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        DetectDeath();
    }

    public void DetectDeath()
    {
        if(_currentHealth <= 0)
            Destroy(gameObject);
    }
}
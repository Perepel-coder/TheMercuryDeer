using System;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    private Enemy _enemy;
    private int _maxHealth;
    private int _currentHealth;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _maxHealth = _enemy.MaxHealth;
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
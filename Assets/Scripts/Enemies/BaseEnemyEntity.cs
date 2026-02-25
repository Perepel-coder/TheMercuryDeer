using UnityEngine;

public class BaseEnemyEntity : MonoBehaviour
{
    private BaseEnemyAI _enemyAI;
    private int _maxHealth;
    private int _currentHealth;

    private void Awake()
    {
        _enemyAI = GetComponent<BaseEnemyAI>();
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
        if (_currentHealth <= 0)
            Destroy(gameObject);
    }
}
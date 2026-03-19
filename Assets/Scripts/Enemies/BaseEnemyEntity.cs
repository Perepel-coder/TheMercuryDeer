using UnityEngine;

public class BaseEnemyEntity : MonoBehaviour
{
    private BaseEnemyAI _enemyAI;
    private int _currentHealth;

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
            Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        DetectDeath();
    }
}
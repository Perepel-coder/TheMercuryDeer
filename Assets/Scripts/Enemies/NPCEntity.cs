using System;
using UnityEngine;

public class NpcEntity : MonoBehaviour
{
    private NpcAI _npcAI;
    private int _maxHealth;
    private int _currentHealth;

    private void Awake()
    {
        _npcAI = GetComponent<NpcAI>();
    }

    private void Start()
    {
        _maxHealth = _npcAI.MaxHealth;
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
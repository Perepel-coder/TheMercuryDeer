using Assets.Scripts.Interfaces.Npc;
using Assets.Scripts.Interfaces.NpcEntity;
using Assets.Scripts.Tools;
using System;
using UnityEngine;

public class BaseEnemyEntity : MonoBehaviour, IDamageable, IHealable
{
    private BaseEnemyAI _ownerAI;
    private int _currentHealth;
    private PopUpDamage _popUpDamage;
    private PopUpDamage _popUpHealth;

    public event EventHandler? OnTakedDamage;
    public event EventHandler? OnDeath;

    public bool IsAlive { get; private set; } = true;

    private void Awake()
    {
        _ownerAI = GetComponent<BaseEnemyAI>();
        _popUpDamage = Resources.Load<PopUpDamage>("Prefabs/Tools/PopUpDamage");
        _popUpHealth = Resources.Load<PopUpDamage>("Prefabs/Tools/PopUpHealth");
    }

    private void Start()
    {
        _currentHealth = _ownerAI.MaxHealth;
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0) Die();
    }

    public void TakeDamage(int damage, Vector3? enemyPosition = null)
    {
        OnTakedDamage?.Invoke(this, EventArgs.Empty);

        _currentHealth -= damage;

        _ownerAI.ReactionToTakingHit?.Weapon.Attack();

        Instantiate(_popUpDamage, _ownerAI.GetTopTransformPosition, Quaternion.identity)
            .DrawDamage(damage,  transform.position.x <= enemyPosition?.x ? Vector2.one : new Vector2(-1, 1));

        DetectDeath();
    }

    public void RestoreHealth(int health)
    {
        _currentHealth += health;

        if(_currentHealth > _ownerAI.MaxHealth)   
            _currentHealth = _ownerAI.MaxHealth;

        Instantiate(_popUpHealth, _ownerAI.GetTopTransformPosition, Quaternion.identity)
            .DrawDamage(health, Vector2.one);
    }

    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);

        IsAlive = false;
    }
}
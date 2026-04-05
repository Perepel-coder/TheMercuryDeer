using System;
using UnityEngine;

namespace Assets.Scripts.Application.Interfaces.Entity
{
    public interface IDamageable
    {
        public event EventHandler OnTakedDamage;

        public event EventHandler OnDeath;

        public void TakeDamage(int damage, Vector3? enemyPosition = null);
        public bool IsAlive { get; }
        public void Die();
    }
}

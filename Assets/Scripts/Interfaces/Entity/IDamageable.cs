using System;
using UnityEngine;

namespace Assets.Scripts.Interfaces.Entity
{
    public interface IDamageable
    {
        public event EventHandler<(int damage, Vector3? enemyPosition)> OnTakedDamage;

        public event EventHandler OnDeath;

        public void TakeDamage(int damage, Vector3? enemyPosition = null);
        public bool IsAlive { get; }
        public void Die();
    }
}

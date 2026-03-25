using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
        public bool IsAlive { get; }
        public void Die();
    }
}

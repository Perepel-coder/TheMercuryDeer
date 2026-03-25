using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enemies.StateHandler
{
    public class BaseAttackingStateHandler : IAttackingStateHandler<BaseEnemyAI>
    {
        public float NextAttackTime { get; private set; }
        public virtual void Run(BaseEnemyAI owner) => NextAttackTime = Time.time + owner.AttackRate;
    }
}

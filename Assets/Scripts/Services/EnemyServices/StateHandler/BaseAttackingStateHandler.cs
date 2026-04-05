using Assets.Scripts.Application.Interfaces.NpcStates;
using UnityEngine;

namespace Assets.Scripts.Services.Enemies.StateHandler
{
    public class BaseAttackingStateHandler : IAttackingStateHandler<BaseEnemyAIService>
    {
        public float NextAttackTime { get; private set; }
        public virtual void Run(BaseEnemyAIService owner) => NextAttackTime = Time.time + owner.Stats.AttackRate;
    }
}

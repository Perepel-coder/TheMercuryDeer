using Assets.Scripts.Interfaces.NpcStates;
using UnityEngine;

namespace Assets.Scripts.Services.Enemies.StateHandler
{
    public class BaseRoamingStateHandler : IRoamingStateHandler<BaseEnemyAIService>
    {
        public Vector3 TargetPosition { get; private set; }

        public void Run(BaseEnemyAIService enemyAI)
        {
            TargetPosition = enemyAI.CurrentPoison +
                Utils.GetRandomDirection() * Random.Range(enemyAI.Stats.RoamingDistanceMin, enemyAI.Stats.RoamingDistanceMax);
        }
    }
}

using Assets.Scripts.Interfaces;
using TheMercuryDeer.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Enemies.StateHandler
{
    public class BaseRoamingStateHandler : IRoamingStateHandler<BaseEnemyAI>
    {
        public Vector3 TargetPosition { get; private set; }

        public void Run(BaseEnemyAI enemyAI)
        {
            TargetPosition = enemyAI.CurrentPoison +
                Utils.GetRandomDirection() * Random.Range(enemyAI.RoamingDistanceMin, enemyAI.RoamingDistanceMax);
        }
    }
}

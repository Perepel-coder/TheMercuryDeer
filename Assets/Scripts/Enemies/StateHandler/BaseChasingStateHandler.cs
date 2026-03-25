using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Enemies.StateHandler
{
    class BaseChasingStateHandler : IChasingStateHandler<BaseEnemyAI>
    {
        public Vector3 TargetPosition { get; private set; }

        public void Run(BaseEnemyAI owner) => TargetPosition = Player.Instance.transform.position;
    }
}

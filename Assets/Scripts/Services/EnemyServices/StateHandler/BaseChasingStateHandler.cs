using Assets.Scripts.Interfaces.NpcStates;
using Assets.Scripts.Services.Player;
using UnityEngine;

namespace Assets.Scripts.Services.Enemies.StateHandler
{
    class BaseChasingStateHandler : IChasingStateHandler<BaseEnemyAIService>
    {
        public Vector3 TargetPosition { get; private set; }

        public void Run(BaseEnemyAIService owner) => TargetPosition = PlayerService.Instance.transform.position;
    }
}

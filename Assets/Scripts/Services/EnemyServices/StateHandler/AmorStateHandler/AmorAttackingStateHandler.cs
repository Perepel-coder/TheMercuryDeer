using Assets.Scripts.Services.Enemies.StateHandler;
using Assets.Scripts.Services.Player;
using Assets.Scripts.Services.Weapons.AmorSword;
using UnityEngine;

namespace Assets.Scripts.Services.Enemies.StateHandler.AmorStateHandler
{
    public class AmorAttackingStateHandler : BaseAttackingStateHandler
    {
        private Vector3 _dropHeight;
        private AmorSwordService _amorSword;

        public AmorAttackingStateHandler(AmorSwordService amorSword, float dropHeight = 1.0f) : base()
        {
            _dropHeight = new Vector3(0, dropHeight, 0);
            _amorSword = amorSword;            
        }

        public override void Run(BaseEnemyAIService owner)
        {
            Vector3 currentPlayerPosition = PlayerService.Instance.transform.position;
            _amorSword.PositionStart = currentPlayerPosition + _dropHeight;
            _amorSword.Attack();
            base.Run(owner);
        }
    }
}

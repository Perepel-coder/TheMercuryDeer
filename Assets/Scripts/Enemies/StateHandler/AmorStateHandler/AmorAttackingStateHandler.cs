using Assets.Scripts.Weapons.AmorSword;
using UnityEngine;

namespace Assets.Scripts.Enemies.StateHandler.AmorStateHandler
{
    public class AmorAttackingStateHandler : BaseAttackingStateHandler
    {
        private Vector3 _dropHeight;
        private AmorSword _amorSword;

        public AmorAttackingStateHandler(AmorSword amorSword, float dropHeight = 1.0f) : base()
        {
            _dropHeight = new Vector3(0, dropHeight, 0);
            _amorSword = amorSword;            
        }

        public override void Run(BaseEnemyAI owner)
        {
            Vector3 currentPlayerPosition = global::Player.Instance.transform.position;
            _amorSword.PositionStart = currentPlayerPosition + _dropHeight;
            _amorSword.Attack();
            base.Run(owner);
        }
    }
}

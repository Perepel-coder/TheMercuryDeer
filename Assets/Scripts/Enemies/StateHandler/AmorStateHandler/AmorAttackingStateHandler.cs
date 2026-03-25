using Assets.Scripts.Weapons.AmorSword;
using UnityEngine;

namespace Assets.Scripts.Enemies.StateHandler.AmorStateHandler
{
    public class AmorAttackingStateHandler : BaseAttackingStateHandler
    {
        private Vector3 _dropHeight;
        public Vector3 SetDropHeight(float dropHeight = 1.0f) => _dropHeight = new Vector3(0, dropHeight, 0);

        public override void Run(BaseEnemyAI owner)
        {
            if (owner.ActiveWeapon == null)
            {
                Debug.LogError("ActiveWeapon is null. Cannot perform attack.");
                return;
            }

            Vector3 currentPlayerPosition = Player.Instance.transform.position;
            var amorSword = owner.ActiveWeapon.Weapon as AmorSword;
            amorSword.PositionStart = currentPlayerPosition + _dropHeight;
            amorSword.Attack();

            Debug.Log($"Sword position: {amorSword.PositionStart} | PlayerPosition: {currentPlayerPosition}");
            base.Run(owner);
        }
    }
}

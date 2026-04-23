using Assets.Scripts.Interfaces.Weapon;
using System;
using UnityEngine;
using static Assets.Scripts.Constants.ItemDefinitions;

namespace Assets.Scripts.Services.WeaponServices
{
    public class AmorSwordService : WeaponService, IFallingWeapon, IMainWeapon
    {
        protected override WeaponTag WeaponTag => WeaponTag.AmorSword;

        private Vector3 _dropHeightVector;

        private Vector3 _positionStart;
        public void SetPositionStart(Vector3 targetPosition) => _positionStart = targetPosition + _dropHeightVector;

        public event EventHandler OnFallAttack;

        protected override void Start()
        {
            base.Start();

            gameObject.SetActive(false);

            _dropHeightVector = new Vector3(0, Stats.DropHeight, 0);
        }

        public override void Attack()
        {
            IsAttacking = true;
            gameObject.SetActive(true);
            gameObject.transform.position = _positionStart;

            OnFallAttack?.Invoke(this, EventArgs.Empty);
        }
    }

}

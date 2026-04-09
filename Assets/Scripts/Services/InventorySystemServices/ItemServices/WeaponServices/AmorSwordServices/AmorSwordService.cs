using Assets.Scripts.Application.Interfaces.Weapon;
using System;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices.ItemServices.WeaponServices.AmorSwordServices
{
    public class AmorSwordService : WeaponService, IFallingWeapon, IMainWeapon
    {
        protected override Tag Tag => Tag.AmorSword;

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

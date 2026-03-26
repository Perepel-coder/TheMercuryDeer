using Assets.Scripts.Interfaces.Weapon;
using System;
using UnityEngine;

namespace Assets.Scripts.Weapons.AmorSword
{
    public class AmorSword : Weapon, IFallingWeapon, IMainWeapon
    {
        public override int DamageAmount { get; protected set; } = 10;

        public Vector3 PositionStart { get; set; }

        public event EventHandler? OnFallAttack;

        private void FixedUpdate()
        {

        }

        protected override void Start()
        {
            base.Start();

            gameObject.SetActive(false);
        }

        public override void Attack()
        {
            IsAttacking = true;
            gameObject.SetActive(true);
            gameObject.transform.position = PositionStart;

            OnFallAttack?.Invoke(this, EventArgs.Empty);
        }
    }
}

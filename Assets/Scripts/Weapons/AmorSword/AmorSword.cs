using System;
using UnityEngine;

namespace Assets.Scripts.Weapons.AmorSword
{
    public class AmorSword : Weapon
    {
        public override int DamageAmount { get; protected set; } = 10;

        public Vector3 PositionStart { get; set; }

        public event EventHandler OnFallAttack;

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
            gameObject.SetActive(true);
            gameObject.transform.position = PositionStart;

            Debug.Log($"Sword position: {gameObject.transform.position}");

            OnFallAttack?.Invoke(this, EventArgs.Empty);
        }
    }
}

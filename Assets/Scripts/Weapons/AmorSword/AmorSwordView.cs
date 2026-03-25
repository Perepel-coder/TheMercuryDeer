using System;
using TheMercuryDeer.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Weapons.AmorSword
{
    public class AmorSwordView : View
    {
        private AmorSword _sword;

        protected override void Awake()
        {
            base.Awake();
            _sword = GetComponentInParent<AmorSword>();
        }

        private void Start()
        {
            _sword.OnFallAttack += _sword_OnFallAttack;
        }

        private void _sword_OnFallAttack(object sender, EventArgs e) => _animator.SetTrigger(Utils.ATTACK);

        public void OnAttackAnimationEnter()
        {
            _sword.TurnOnCollider(true);
        }

        public void OnAttackAnimationExit()
        {
            _sword.TurnOnCollider(false);
            _sword.gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
        }
    }
}

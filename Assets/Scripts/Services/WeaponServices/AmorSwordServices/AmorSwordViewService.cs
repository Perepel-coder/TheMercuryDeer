using Assets.Scripts.Paths;
using System;
using UnityEngine;

namespace Assets.Scripts.Services.Weapons.AmorSword
{
    public class AmorSwordViewService : View
    {
        private AmorSwordService _sword;

        protected override void Awake()
        {
            base.Awake();
            _sword = GetComponentInParent<AmorSwordService>();
        }

        private void Start()
        {
            _sword.OnFallAttack += _sword_OnFallAttack;
        }

        private void OnDestroy()
        {
            _sword.OnFallAttack -= _sword_OnFallAttack;
        }

        private void _sword_OnFallAttack(object sender, EventArgs e) => _animator.SetTrigger(AnimatorParameters.ATTACK);


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

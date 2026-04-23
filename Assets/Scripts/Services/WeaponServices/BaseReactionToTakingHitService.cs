using Assets.Scripts.Interfaces.Entity;
using Assets.Scripts.Interfaces.Weapon;
using UnityEngine;
using static Assets.Scripts.Constants.ItemDefinitions;

namespace Assets.Scripts.Services.WeaponServices
{
    public class BaseReactionToTakingHitService : WeaponService, IDamageReaction
    {
        private BaseEnemyAIService _ownerAI;
        private BaseEntityService _ownerEntity;

        private int _damageAmount;
        private float _healthAmount;

        private enum Reaction
        {
            DealingDamage,
            HealthRecovery,
        }

        private Reaction? _currentReaction;

        protected override void Start()
        {
            base.Start();
            _ownerAI = GetComponentInParent<BaseEnemyAIService>();
            _ownerEntity = GetComponentInParent<BaseEntityService>();
        }

        protected override WeaponTag WeaponTag => WeaponTag.BaseReactionToTakingHit;

        private void ClearReactionState()
        {
            _damageAmount = 0;
            _healthAmount = 0;
            _currentReaction = null;
        }

        public override void Attack()
        {
            ClearReactionState();
            _currentReaction = Utils.GetRandomEnumValue<Reaction>();

            int amountReaction = Random.Range(0, _ownerAI.Stats.InherentDamage + 1);

            switch (_currentReaction)
            {
                case Reaction.DealingDamage: _damageAmount = amountReaction; break;
                case Reaction.HealthRecovery: _healthAmount = amountReaction; break;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out IDamageable enemy) && !Stats.IsContinuousDamage)
                enemy.TakeDamage(_damageAmount);


            if (collision.transform.TryGetComponent(out IDamageable _) && _currentReaction is Reaction.HealthRecovery)
                _ownerEntity.RestoreHealth(_healthAmount);

            ClearReactionState();
        }
    }
}

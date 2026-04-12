using Assets.Scripts.Application.Interfaces.Entity;
using Assets.Scripts.Application.Interfaces.Weapon;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices.ItemServices.WeaponServices.ReactionToTakingHitServices
{
    public class BaseReactionToTakingHitService : WeaponService, IDamageReaction
    {
        private BaseEnemyAIService _ownerAI;
        private BaseEntityService _ownerEntity;

        private enum Reaction
        {
            DealingDamage,
            HealthRecovery,
        }

        private Reaction _currentReaction;

        protected override void Start()
        {
            base.Start();
            _ownerAI = GetComponentInParent<BaseEnemyAIService>();
            _ownerEntity = GetComponentInParent<BaseEntityService>();
        }

        protected override Tag Tag => Tag.BaseReactionToTakingHit;

        private void ClearReactions()
        {
            Stats.DamageAmount = 0;
            Stats.HealthAmount = 0;
        }

        public override void Attack()
        {
            ClearReactions();

            _currentReaction = Utils.GetRandomEnumValue<Reaction>();

            int amountReaction = Random.Range(0, _ownerAI.Stats.InherentDamage + 1);

            switch (_currentReaction)
            {
                case Reaction.DealingDamage: Stats.DamageAmount = amountReaction; break;
                case Reaction.HealthRecovery: Stats.HealthAmount = amountReaction; break;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.transform.TryGetComponent(out IDamageable _) &&
                _currentReaction is Reaction.HealthRecovery)
                _ownerEntity.RestoreHealth(Stats.HealthAmount);
        }
    }
}

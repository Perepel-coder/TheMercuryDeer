using Assets.Scripts.Application.Interfaces.Entity;
using Assets.Scripts.Application.Interfaces.Weapon;
using UnityEngine;

namespace Assets.Scripts.Services.Weapons.ReactionToTakingHit
{
    public class BaseReactionToTakingHitService : WeaponService, IDamageReaction
    {
        private BaseEnemyAIService _ownerAI;
        private BaseEntityService _ownerEntity;

        public override bool IsContinuousDamage => false;

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

        public override int DamageAmount { get; protected set; } = 0;
        public virtual int HealthAmount { get; protected set; } = 0;

        private void ClearReactions()
        {
            DamageAmount = 0;
            HealthAmount = 0;
        }

        public override void Attack()
        {
            ClearReactions();

            _currentReaction = Utils.GetRandomEnumValue<Reaction>();

            int amountReaction = Random.Range(0, _ownerAI.InherentDamage + 1);

            switch (_currentReaction)
            {
                case Reaction.DealingDamage: DamageAmount = amountReaction; break;
                case Reaction.HealthRecovery: HealthAmount = amountReaction; break;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.transform.TryGetComponent(out IDamageable _) &&
                _currentReaction is Reaction.HealthRecovery)
                _ownerEntity.RestoreHealth(HealthAmount);
        }
    }
}

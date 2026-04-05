using Assets.Scripts.Interfaces.Npc;
using Assets.Scripts.Interfaces.Weapon;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Weapons.ReactionToTakingHit
{
    public class BaseReactionToTakingHit : Weapon, IDamageReaction
    {
        private BaseEnemyAI _ownerAI;
        private BaseEntity _ownerEntity;

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
            _ownerAI = GetComponentInParent<BaseEnemyAI>();
            _ownerEntity = GetComponentInParent<BaseEntity>();
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

using UnityEngine;

namespace Assets.Scripts.Services.Enemies
{
    public class BaseEnemyEntityService : BaseEntityService
    {
        private new BaseEnemyAIService _ownerAI;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            _ownerAI = base._ownerAI as BaseEnemyAIService;
        }

        public override void TakeDamage(int damage, Vector3? enemyPosition = null)
        {
            _ownerAI.ReactionToTakingHit?.Weapon.Attack();

            base.TakeDamage(damage, enemyPosition);
        }
    }
}

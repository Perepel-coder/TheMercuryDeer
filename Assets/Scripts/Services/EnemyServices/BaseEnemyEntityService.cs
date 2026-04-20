using Assets.Scripts.Constans.Paths;
using Assets.Scripts.Services.UI;
using UnityEngine;

namespace Assets.Scripts.Services.Enemies
{
    public class BaseEnemyEntityService : BaseEntityService
    {
        private new BaseEnemyAIService _ownerAI;
        private PopUpDamageService _popUpDamage;
        private PopUpDamageService _popUpHealth;

        protected override void Awake()
        {
            _popUpDamage = Resources.Load<PopUpDamageService>(ResourcePaths.UI.DAMAGE_POP_UP);
            _popUpHealth = Resources.Load<PopUpDamageService>(ResourcePaths.UI.HEALTH_POP_UP);

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

            Instantiate(_popUpDamage, _ownerAI.GetTopTransformPosition, Quaternion.identity)
                .DrawDamage(damage, transform.position.x <= enemyPosition?.x ? Vector2.one : new Vector2(-1, 1));

            base.TakeDamage(damage, enemyPosition);
        }

        public override void RestoreHealth(float health)
        {
            Instantiate(_popUpHealth, _ownerAI.GetTopTransformPosition, Quaternion.identity)
                .DrawDamage(health, Vector2.one);

            base.RestoreHealth(health);
        }
    }
}

using Assets.Scripts.Paths;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class BaseEnemyEntity : BaseEntity
    {
        private new BaseEnemyAI _ownerAI;
        private PopUpDamage _popUpDamage;
        private PopUpDamage _popUpHealth;

        protected override void Awake()
        {
            _popUpDamage = Resources.Load<PopUpDamage>(ResourcePaths.UI.DAMAGE_POP_UP);
            _popUpHealth = Resources.Load<PopUpDamage>(ResourcePaths.UI.HEALTH_POP_UP);

            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
            _ownerAI = base._ownerAI as BaseEnemyAI;
        }

        public override void TakeDamage(int damage, Vector3? enemyPosition = null)
        {
            _ownerAI.ReactionToTakingHit?.Weapon.Attack();

            Instantiate(_popUpDamage, _ownerAI.GetTopTransformPosition, Quaternion.identity)
                .DrawDamage(damage, transform.position.x <= enemyPosition?.x ? Vector2.one : new Vector2(-1, 1));

            base.TakeDamage(damage, enemyPosition);
        }

        public override void RestoreHealth(int health)
        {
            Instantiate(_popUpHealth, _ownerAI.GetTopTransformPosition, Quaternion.identity)
                .DrawDamage(health, Vector2.one);

            base.RestoreHealth(health);
        }
    }
}

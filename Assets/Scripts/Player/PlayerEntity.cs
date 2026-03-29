using UnityEngine;


namespace Assets.Scripts.Player
{
    public class PlayerEntity : BaseEntity
    {
        protected override void Start()
        {
            base.Start();
            GameMainCanvas.HealthSlider.maxValue = _ownerAI.MaxHealth;
            GameMainCanvas.HealthSlider.value = _ownerAI.CurrentHealth;
        }

        public override void TakeDamage(int damage, Vector3? enemyPosition = null)
        {
            base.TakeDamage(damage, enemyPosition);

            GameMainCanvas.HealthSlider.value = _ownerAI.CurrentHealth;
        }

        public override void RestoreHealth(int health)
        {
            base.RestoreHealth(health);

            GameMainCanvas.HealthSlider.value = _ownerAI.CurrentHealth;
        }
    }
}

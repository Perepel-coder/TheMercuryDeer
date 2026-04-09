using UnityEngine;


namespace Assets.Scripts.Services.Player
{
    public class PlayerEntityService : BaseEntityService
    {
        public static PlayerEntityService Instance { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override void Start()
        {
            base.Start();
            GameMainCanvasService.HealthSlider.maxValue = _ownerAI.MaxHealth;
            GameMainCanvasService.HealthSlider.value = _ownerAI.CurrentHealth;
        }

        public override void TakeDamage(int damage, Vector3? enemyPosition = null)
        {
            base.TakeDamage(damage, enemyPosition);

            GameMainCanvasService.HealthSlider.value = _ownerAI.CurrentHealth;
        }

        public override void RestoreHealth(float health)
        {
            base.RestoreHealth(health);

            GameMainCanvasService.HealthSlider.value = _ownerAI.CurrentHealth;
        }
    }
}

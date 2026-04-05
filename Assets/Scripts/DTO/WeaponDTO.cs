using Assets.Scripts.Enums;

namespace Assets.Scripts.DTO
{
    public class WeaponDTO
    {
        public int Id { get; set; }
        public WeaponTag Tag { get; set; }
        public int HealthAmount { get; set; }
        public int DamageAmount { get; set; }
        public bool IsContinuousDamage { get; set; }
        public float DropHeight { get; set; }
        public int PlayerId { get; set; } = -1;
        public int EnemyId { get; set; } = -1;
    }
}

namespace Assets.Scripts.DTO
{
    public class EnemyDTO
    {
        public int Id { get; set; }
        public EnemyName Name { get; set; }

        public float RoamingDistanceMin { get; set; } = 2f;
        public float RoamingDistanceMax { get; set; } = 6f;
        public float RoamingTimeMax { get; set; } = 4f;
        public float RoamingSpeed { get; set; } = 1f;

        public float ChasingDistance { get; set; } = 5f;
        public float ChasingSpeedMultiplier { get; set; } = 2f;

        public float AttackingDistance { get; set; } = 0.5f;
        public float AttackRate { get; set; } = 2f;
        public float NextAttackTime { get; set; } = 0f;

        public int InherentDamage { get; set; } = 1;

        public int MaxHealth { get; set; } = 30;
    }
}

using SQLite;
using static Assets.Scripts.Enums.EnemyEnums.EnemyDefinitions;

namespace Assets.Scripts.Models
{
    [Table("Enemy")]
    public class Enemy
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public EnemyTag Tag { get; set; }

        [Column("RoamingDistanceMin")]
        public float RoamingDistanceMin { get; set; } = 2f;
        [Column("RoamingDistanceMax")]
        public float RoamingDistanceMax { get; set; } = 6f;
        [Column("RoamingTimeMax")]
        public float RoamingTimeMax { get; set; } = 4f;
        [Column("RoamingSpeed")]
        public float RoamingSpeed { get; set; } = 1f;

        [Column("ChasingDistance")]
        public float ChasingDistance { get; set; } = 5f;
        [Column("ChasingSpeedMultiplier")]
        public float ChasingSpeedMultiplier { get; set; } = 2f;

        [Column("AttackingDistance")]
        public float AttackingDistance { get; set; } = 0.5f;
        [Column("AttackRate")]
        public float AttackRate { get; set; } = 2f;
        [Column("InherentDamage")]
        public int InherentDamage { get; set; } = 1;

        [Column("MaxHealth")]
        public int MaxHealth { get; set; } = 30;
    }
}
using SQLite;

namespace Assets.Scripts.Models
{
    [Table("Enemy")]
    public class Enemy
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("RoamingDistanceMin")]
        public float RoamingDistanceMin { get; set; }
        [Column("RoamingDistanceMax")]
        public float RoamingDistanceMax { get; set; }
        [Column("RoamingTimeMax")]
        public float RoamingTimeMax { get; set; }
        [Column("RoamingSpeed")]
        public float RoamingSpeed { get; set; }

        [Column("ChasingDistance")]
        public float ChasingDistance { get; set; }
        [Column("ChasingSpeedMultiplier")]
        public float ChasingSpeedMultiplier { get; set; }

        [Column("AttackingDistance")]
        public float AttackingDistance { get; set; }
        [Column("AttackRate")]
        public float AttackRate { get; set; }
        [Column("InherentDamage")]
        public int InherentDamage { get; set; }

        [Column("MaxHealth")]
        public int MaxHealth { get; set; }
    }
}

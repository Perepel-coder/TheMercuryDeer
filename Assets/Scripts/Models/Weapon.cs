using Assets.Scripts.Enums;
using SQLite;

namespace Assets.Scripts.Models
{
    [Table("Weapon")]
    public class Weapon
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Tag")]
        public WeaponTag Tag { get; set; }

        [Column("HealthAmount")]
        public int HealthAmount { get; set; }

        [Column("DamageAmount")]
        public int DamageAmount { get; set; }

        [Column("IsContinuousDamage")]
        public bool IsContinuousDamage { get; set; }

        [Column("DropHeight")]
        public float DropHeight { get; set; }

        [Column("PlayerId")]
        public int PlayerId { get; set; } = -1;

        [Column("EnemyId")]
        public int EnemyId { get; set; } = -1;
    }
}
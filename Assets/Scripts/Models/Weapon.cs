using SQLite;

namespace Assets.Scripts.Models
{
    [Table("Weapon")]
    public class Weapon
    {
        [PrimaryKey, AutoIncrement]
        [Column("Id")]
        public int Id { get; set; }

        [Column("DamageAmount")]
        public int DamageAmount { get; set; }

        [Column("IsContinuousDamage")]
        public bool IsContinuousDamage { get; set; }
    }
}
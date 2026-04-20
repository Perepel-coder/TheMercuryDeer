using SQLite;

namespace Assets.Scripts.Models
{
    [Table("Player")]
    public class Player : BaseModel
    {
        [Column("MaxHealth")]
        public int MaxHealth { get; set; }


        [Column("BaseSpeedMoveing")]
        public float BaseSpeedMoveing { get; set; }
        [Column("SpeedMoveingMin")]
        public float SpeedMoveingMin { get; set; }


        [Column("DashSpeedMultiplier")]
        public int DashSpeedMultiplier { get; set; }
        [Column("DashDuration")]
        public float DashDuration { get; set; }
        [Column("DashCooldown")]
        public float DashCooldown { get; set; }
    }
}

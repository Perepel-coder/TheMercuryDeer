namespace Assets.Scripts.DTO
{
    public class PlayerDTO
    {
        public int Id { get; set; }
        public int MaxHealth { get; set; }
        public float BaseSpeedMoveing { get; set; }
        public float SpeedCurrrentMoveing { get; set; }
        public float SpeedMoveingMin { get; set; }
        public int DashSpeedMultiplier { get; set; }
        public float DashDuration { get; set; }
        public float DashCooldown { get; set; }
    }
}

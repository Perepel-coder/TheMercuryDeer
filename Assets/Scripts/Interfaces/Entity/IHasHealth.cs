namespace Assets.Scripts.Interfaces.Entity
{
    public interface IHasHealth
    {
        public float CurrentHealth { get; set; }
        public float MaxHealth { get; }
    }
}
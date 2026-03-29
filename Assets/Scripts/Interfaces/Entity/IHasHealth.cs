namespace Assets.Scripts.Interfaces.Entity
{
    public interface IHasHealth
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; }
    }
}
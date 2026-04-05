namespace Assets.Scripts.Application.Interfaces.Entity
{
    public interface IHasHealth
    {
        public int CurrentHealth { get; set; }
        public int MaxHealth { get; }
    }
}
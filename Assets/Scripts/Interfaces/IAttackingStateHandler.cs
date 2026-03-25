namespace Assets.Scripts.Interfaces
{
    public interface IAttackingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public float NextAttackTime { get; }
    }
}

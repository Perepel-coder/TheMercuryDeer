namespace Assets.Scripts.Interfaces.IStateHandler
{
    public interface IAttackingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public float NextAttackTime { get; }
    }
}

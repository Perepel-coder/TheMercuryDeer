namespace Assets.Scripts.Interfaces.NpcStates
{
    public interface IAttackingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public float NextAttackTime { get; }
    }
}

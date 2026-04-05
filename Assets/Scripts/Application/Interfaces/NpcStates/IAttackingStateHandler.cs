namespace Assets.Scripts.Application.Interfaces.NpcStates
{
    public interface IAttackingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public float NextAttackTime { get; }
    }
}

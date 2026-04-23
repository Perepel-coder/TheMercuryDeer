namespace Assets.Scripts.Interfaces.NpcStates
{
    public interface IStateHandler<T> where T : IHasState
    {
        public void Run(T owner);
    }
}
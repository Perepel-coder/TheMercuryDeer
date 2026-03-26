namespace Assets.Scripts.Interfaces.IStateHandler
{
    public interface IStateHandler<T> where T : IHasState
    {
        public void Run(T owner);
    }
}
namespace Assets.Scripts.Interfaces
{
    public interface IStateHandler<T> where T : IHasState
    {
        public void Run(T owner);
    }
}
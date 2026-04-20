namespace Assets.Scripts.Application.Interfaces.NpcStates
{
    public interface IHasState
    {
        void StateHandler();
        void CheckCurrentState();
    }
}

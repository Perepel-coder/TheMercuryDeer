using UnityEngine;

namespace Assets.Scripts.Interfaces.NpcStates
{
    public interface IRoamingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public Vector3 TargetPosition { get; }
    }
}

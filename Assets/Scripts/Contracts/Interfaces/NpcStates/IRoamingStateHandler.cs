using UnityEngine;

namespace Assets.Scripts.Application.Interfaces.NpcStates
{
    public interface IRoamingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public Vector3 TargetPosition { get; }
    }
}

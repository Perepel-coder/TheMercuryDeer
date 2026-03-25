using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IRoamingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public Vector3 TargetPosition { get; }
    }
}

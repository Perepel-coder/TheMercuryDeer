using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IChasingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public Vector3 TargetPosition { get; }
    }
}

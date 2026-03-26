using UnityEngine;

namespace Assets.Scripts.Interfaces.IStateHandler
{
    public interface IChasingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public Vector3 TargetPosition { get; }
    }
}

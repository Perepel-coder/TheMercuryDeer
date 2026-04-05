using UnityEngine;

namespace Assets.Scripts.Application.Interfaces.NpcStates
{
    public interface IChasingStateHandler<T> : IStateHandler<T> where T : IHasState
    {
        public Vector3 TargetPosition { get; }
    }
}

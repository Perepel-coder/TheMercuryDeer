using Assets.Scripts.Enemies.StateHandler.AmorStateHandler;
using TheMercuryDeer.Scripts.Enemy;
using UnityEngine;

public class AmorAI : BaseEnemyAI
{
    public override int MaxHealth => 20;
    public override bool IsChasingEnemy => true;

    public override bool IsEnemy => true;

    protected override void Start()
    {
        base.Start();
        _currentState = State.Roaming;

        AttackingDistance = 2.5f;

        _attackingStateHandler = new AmorAttackingStateHandler();
        (_attackingStateHandler as AmorAttackingStateHandler).SetDropHeight(2f);
    }

    protected override void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition)
    {
        if(_currentState != State.Attacking)
            base.ChangeFacingDirection(currentPosition, targetPosition);
    }
}
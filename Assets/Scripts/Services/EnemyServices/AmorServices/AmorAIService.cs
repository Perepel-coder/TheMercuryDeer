using Assets.Scripts.Services.Enemies;
using Assets.Scripts.Services.Enemies.StateHandler.AmorStateHandler;
using Assets.Scripts.Services.Weapons.AmorSword;
using UnityEngine;

public class AmorAIService : BaseEnemyAIService
{
    [SerializeField] private float _swordDropHeight = 3f;
    public override int MaxHealth => 50;
    public override bool IsChasingEnemy => true;

    public override bool IsEnemy => true;

    protected override void Start()
    {
        base.Start();
        _currentState = State.Roaming;

        AttackingDistance = 2.5f;
        InherentDamage = 5;

        _attackingStateHandler = new AmorAttackingStateHandler((AmorSwordService)ActiveWeapon!.Weapon, _swordDropHeight);
    }

    protected override bool CheckAttackingState(float distanceToPlayer) =>
        IsEnemy && (distanceToPlayer <= AttackingDistance || (ActiveWeapon?.Weapon.IsAttacking ?? false));

    protected override void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition)
    {
        if (!ActiveWeapon!.Weapon.IsAttacking)
            base.ChangeFacingDirection(currentPosition, targetPosition);
    }
}
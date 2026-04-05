using Assets.Scripts.DTO;
using Assets.Scripts.Services.Enemies.StateHandler.AmorStateHandler;
using Assets.Scripts.Services.Weapons.AmorSword;
using UnityEngine;

public class AmorAIService : BaseEnemyAIService
{
    [SerializeField] private float _swordDropHeight = 3f;

    protected override EnemyName Name { get; } = EnemyName.Amor;

    protected override void Start()
    {
        base.Start();

        _attackingStateHandler = new AmorAttackingStateHandler((AmorSwordService)ActiveWeapon!.Weapon, _swordDropHeight);
    }

    protected override bool CheckAttackingState(float distanceToPlayer) =>
        (distanceToPlayer <= Stats.AttackingDistance || (ActiveWeapon?.Weapon.IsAttacking ?? false));

    protected override void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition)
    {
        if (!ActiveWeapon!.Weapon.IsAttacking)
            base.ChangeFacingDirection(currentPosition, targetPosition);
    }
}
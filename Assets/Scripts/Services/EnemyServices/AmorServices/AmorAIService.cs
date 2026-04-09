using Assets.Scripts.Services.Enemies.StateHandler.AmorStateHandler;
using Assets.Scripts.Services.InventorySystemServices.ItemServices.WeaponServices.AmorSwordServices;
using UnityEngine;
using static Assets.Scripts.Enums.EnemyEnums.EnemyDefinitions;

public class AmorAIService : BaseEnemyAIService
{
    protected override EnemyTag Name { get; } = EnemyTag.Amor;

    protected override void Start()
    {
        base.Start();

        _attackingStateHandler = new AmorAttackingStateHandler((AmorSwordService)ActiveWeapon!.Weapon);
    }

    protected override bool CheckAttackingState(float distanceToPlayer) =>
        (distanceToPlayer <= Stats.AttackingDistance || (ActiveWeapon?.Weapon.IsAttacking ?? false));

    protected override void ChangeFacingDirection(Vector3 currentPosition, Vector3 targetPosition)
    {
        if (!ActiveWeapon!.Weapon.IsAttacking)
            base.ChangeFacingDirection(currentPosition, targetPosition);
    }
}
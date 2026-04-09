using System;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

public class PlayerSwordService : WeaponService
{
    protected override Tag Tag => Tag.PlayerSword;

    public event EventHandler OnSwing;

    public override void Attack()
    {
        TurnOnCollider(false);
        TurnOnCollider(true);

        OnSwing?.Invoke(this, EventArgs.Empty);
    }
}
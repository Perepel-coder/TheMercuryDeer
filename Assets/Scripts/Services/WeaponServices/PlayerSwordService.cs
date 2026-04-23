using System;
using static Assets.Scripts.Constants.ItemDefinitions;

public class PlayerSwordService : WeaponService
{
    protected override WeaponTag WeaponTag => WeaponTag.PlayerSword;

    public event EventHandler OnSwing;

    public override void Attack()
    {
        TurnOnCollider(false);
        TurnOnCollider(true);

        OnSwing?.Invoke(this, EventArgs.Empty);
    }
}
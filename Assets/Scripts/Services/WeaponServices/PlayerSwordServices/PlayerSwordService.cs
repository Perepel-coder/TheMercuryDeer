using Assets.Scripts.Enums;
using System;

public class PlayerSwordService : WeaponService
{
    protected override WeaponTag Tag => WeaponTag.PlayerSword;

    public event EventHandler OnSwing;

    public override void Attack()
    {
        TurnOnCollider(false);
        TurnOnCollider(true);

        OnSwing?.Invoke(this, EventArgs.Empty);
    }
}
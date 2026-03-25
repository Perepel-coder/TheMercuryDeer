using System;

public class Sword : Weapon
{
    public override int DamageAmount { get; protected set; } = 5;

    public event EventHandler? OnSwing;

    public override void Attack()
    {
        TurnOnCollider(false);
        TurnOnCollider(true);

        OnSwing?.Invoke(this, EventArgs.Empty);
    }
}
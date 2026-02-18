using UnityEngine;

public class Seraphim : Enemy
{
    public static Seraphim Instance { get; private set; }
    public override int MaxHealth { get; protected set; } = 20;

    private void Awake() => Instance = this;
}
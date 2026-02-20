using TheMercuryDeer.Scripts.Enemy;

public class SeraphimAI : EnemyAI
{
    public override int MaxHealth { get; } = 20;
    public override bool IsChasingEnemy { get; } = true;

    protected override void Start()
    {
        base.Start();

        _state = State.Roaming;
    }
}
using TheMercuryDeer.Scripts.Enemy;

public class AmorAI : NpcAI
{
    public override int MaxHealth => 20;
    public override bool IsChasingEnemy => false;

    public override bool IsEnemy => true;

    protected override void Start()
    {
        base.Start();

        _currentState = State.Roaming;
    }
}

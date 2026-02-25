using TheMercuryDeer.Scripts.Enemy;

public class AmorAI : BaseEnemyAI
{
    public override int MaxHealth => 20;
    public override bool IsChasingEnemy => true;

    public override bool IsEnemy => true;

    protected override void Start()
    {
        base.Start();
    }
}

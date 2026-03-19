
public class AmorAI : BaseEnemyAI
{
    public override int MaxHealth => 20;
    public override bool IsChasingEnemy => true;

    public override bool IsEnemy => true;

    protected override void Start()
    {
        base.Start();
        _roamingDistanceMax = 7f;
        _nextAttackTime = 0f;
    }
}

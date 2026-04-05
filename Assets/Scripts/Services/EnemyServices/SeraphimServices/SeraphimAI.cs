using Assets.Scripts.Enums;

public class SeraphimAI : BaseEnemyAIService
{
    protected override EnemyTag Name { get; } = EnemyTag.Seraphim;

    protected override void Start()
    {
        base.Start();
    }
}
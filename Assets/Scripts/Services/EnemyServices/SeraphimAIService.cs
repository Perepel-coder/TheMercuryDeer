using static Assets.Scripts.Constants.EnemyDefinitions;

public class SeraphimAIService : BaseEnemyAIService
{
    protected override EnemyTag EnemyTag { get; } = EnemyTag.Seraphim;

    protected override void Start()
    {
        base.Start();
    }
}
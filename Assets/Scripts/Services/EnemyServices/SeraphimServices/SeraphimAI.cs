using Assets.Scripts.DTO;

public class SeraphimAI : BaseEnemyAIService
{
    protected override EnemyName Name { get; } = EnemyName.Seraphim;

    protected override void Start()
    {
        base.Start();
    }
}
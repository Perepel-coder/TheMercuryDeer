using TheMercuryDeer.Scripts.Utils;

public class BaseEnemyView : View
{
    private BaseEnemyAI _enemyAI;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _enemyAI = GetComponentInParent<BaseEnemyAI>();
        _enemyAI.OnEnemyAttacked += _enemyAI_OnEnemyAttacked;
    }

    private void OnDestroy() => _enemyAI.OnEnemyAttacked -= _enemyAI_OnEnemyAttacked;

    private void _enemyAI_OnEnemyAttacked(object sender, System.EventArgs e) => _animator.SetTrigger(Utils.ATTACK);

    private void Update()
    {
        _animator.SetBool(Utils.IS_RUNNING, _enemyAI.IsRunning);

        _animator.SetFloat(Utils.CHASING_SPEED_MULTIPLIER, _enemyAI.ChasingSpeedMultiplier);
    }
}
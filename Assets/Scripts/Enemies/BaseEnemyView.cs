using TheMercuryDeer.Scripts.Utils;

public class BaseEnemyView : View
{
    protected BaseEnemyAI _ownerAI;
    protected BaseEnemyEntity _ownerEntity;

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        _ownerAI = GetComponentInParent<BaseEnemyAI>();
        _ownerEntity = GetComponentInParent<BaseEnemyEntity>();

        _ownerAI.OnEnemyAttacked += _enemyAI_OnEnemyAttacked;
        _ownerEntity.OnTakedDamage += _enemyEntity_OnTakedDamage;
        _ownerEntity.OnDeath += _ownerEntity_OnDeath;
    }

    protected virtual void Update()
    {
        _animator.SetBool(Utils.IS_RUNNING, _ownerAI.IsRunning);

        _animator.SetFloat(Utils.CHASING_SPEED_MULTIPLIER, _ownerAI.ChasingSpeedMultiplier);
    }

    protected virtual void OnDestroy()
    {
        _ownerAI.OnEnemyAttacked -= _enemyAI_OnEnemyAttacked;
        _ownerEntity.OnTakedDamage -= _enemyEntity_OnTakedDamage;
    }

    private void _enemyAI_OnEnemyAttacked(object sender, System.EventArgs e) => _animator.SetTrigger(Utils.ATTACK);

    private void _enemyEntity_OnTakedDamage(object sender, System.EventArgs e) => _animator.SetTrigger(Utils.TAKE_HIT);

    private void _ownerEntity_OnDeath(object sender, System.EventArgs e) => _animator.SetBool(Utils.IS_DIE, true);
}
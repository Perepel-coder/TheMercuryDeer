using Assets.Scripts.Constans.Paths;
using UnityEngine;

public class BaseEnemyViewService : View
{
    protected BaseEnemyAIService _ownerAI;
    protected BaseEntityService _ownerEntity;

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        _ownerAI = GetComponentInParent<BaseEnemyAIService>();
        _ownerEntity = GetComponentInParent<BaseEntityService>();

        _ownerAI.OnEnemyAttacked += _enemyAI_OnEnemyAttacked;
        _ownerEntity.OnTakedDamage += _enemyEntity_OnTakedDamage;
        _ownerEntity.OnDeath += _ownerEntity_OnDeath;
    }

    protected virtual void Update()
    {
        _animator.SetBool(AnimatorParameters.IS_RUNNING, _ownerAI.IsRunning);

        _animator.SetFloat(AnimatorParameters.CHASING_SPEED_MULTIPLIER, _ownerAI.Stats.ChasingSpeedMultiplier);
    }

    protected virtual void OnDestroy()
    {
        _ownerAI.OnEnemyAttacked -= _enemyAI_OnEnemyAttacked;
        _ownerEntity.OnTakedDamage -= _enemyEntity_OnTakedDamage;
        _ownerEntity.OnDeath -= _ownerEntity_OnDeath;
    }

    private void _enemyAI_OnEnemyAttacked(object sender, System.EventArgs e) => _animator.SetTrigger(AnimatorParameters.ATTACK);

    private void _enemyEntity_OnTakedDamage(object sender, System.EventArgs e)
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(AnimationPaths.TAKE_HIT) && stateInfo.normalizedTime < 1f)
            return;

        _animator.SetTrigger(AnimatorParameters.TAKE_HIT);
    }

    private void _ownerEntity_OnDeath(object sender, System.EventArgs e)
    {
        _spriteRenderer.sortingOrder = -1;
        _animator.SetBool(AnimatorParameters.IS_DIE, true);
    }
}
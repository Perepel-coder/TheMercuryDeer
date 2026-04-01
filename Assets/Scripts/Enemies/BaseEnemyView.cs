using Assets.Scripts.Paths;
using Assets.Scripts.Tools;
using UnityEngine;

public class BaseEnemyView : View
{
    protected BaseEnemyAI _ownerAI;
    protected BaseEntity _ownerEntity;

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        _ownerAI = GetComponentInParent<BaseEnemyAI>();
        _ownerEntity = GetComponentInParent<BaseEntity>();

        _ownerAI.OnEnemyAttacked += _enemyAI_OnEnemyAttacked;
        _ownerEntity.OnTakedDamage += _enemyEntity_OnTakedDamage;
        _ownerEntity.OnDeath += _ownerEntity_OnDeath;
    }

    protected virtual void Update()
    {
        _animator.SetBool(AnimatorParameters.IS_RUNNING, _ownerAI.IsRunning);

        _animator.SetFloat(AnimatorParameters.CHASING_SPEED_MULTIPLIER, _ownerAI.ChasingSpeedMultiplier);
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
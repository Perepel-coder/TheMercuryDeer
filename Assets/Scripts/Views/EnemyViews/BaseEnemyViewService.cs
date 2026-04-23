using Assets.Scripts.Constants.Paths;
using Assets.Scripts.Views.UIServices;
using UnityEngine;

public class BaseEnemyViewService : View
{
    protected BaseEnemyAIService _ownerAI;
    protected BaseEntityService _ownerEntity;

    private PopUpDamageService _popUpDamage;
    private PopUpDamageService _popUpHealth;

    protected override void Awake()
    {
        base.Awake();

        _popUpDamage = Resources.Load<PopUpDamageService>(ResourcePaths.UI.DAMAGE_POP_UP);
        _popUpHealth = Resources.Load<PopUpDamageService>(ResourcePaths.UI.HEALTH_POP_UP);
    }

    protected virtual void Start()
    {
        _ownerAI = GetComponentInParent<BaseEnemyAIService>();
        _ownerEntity = GetComponentInParent<BaseEntityService>();

        _ownerAI.OnEnemyAttacked += _enemyAI_OnEnemyAttacked;
        _ownerEntity.OnTakedDamage += _enemyEntity_OnTakedDamage;
        _ownerEntity.OnRestoreHealth += _ownerEntity_OnRestoreHealth;
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

    private void _enemyEntity_OnTakedDamage(object sender, (int damage, Vector3? enemyPosition) args)
    {
        Instantiate(_popUpDamage, _ownerAI.GetTopTransformPosition, Quaternion.identity)
            .DrawDamage(args.damage, transform.position.x <= args.enemyPosition?.x ? Vector2.one : new Vector2(-1, 1));

        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName(AnimationPaths.TAKE_HIT) && stateInfo.normalizedTime < 1f)
            return;

        _animator.SetTrigger(AnimatorParameters.TAKE_HIT);
    }

    private void _ownerEntity_OnRestoreHealth(object sender, float health)
    {
        Instantiate(_popUpHealth, _ownerAI.GetTopTransformPosition, Quaternion.identity)
            .DrawDamage(health, Vector2.one);
    }

    private void _ownerEntity_OnDeath(object sender, System.EventArgs e)
    {
        _spriteRenderer.sortingOrder = -1;
        _animator.SetBool(AnimatorParameters.IS_DIE, true);
    }
}
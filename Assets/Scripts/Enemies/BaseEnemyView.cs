using TheMercuryDeer.Scripts.Utils;
using UnityEngine;

public class BaseEnemyView: View
{
    private BaseEnemyAI _enemyAI;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _enemyAI = GetComponentInParent<BaseEnemyAI>();
    }

    private void Update()
    {
        _animator.SetBool(Utils.IS_RUNNING, _enemyAI.IsRunning);
        //_animator.SetBool(Utils.IS_DIE, _enemy.IsDie);

        _animator.SetFloat(Utils.CHASING_SPEED_MULTIPLIER, _enemyAI.ChasingSpeedMultiplier);
    }
}
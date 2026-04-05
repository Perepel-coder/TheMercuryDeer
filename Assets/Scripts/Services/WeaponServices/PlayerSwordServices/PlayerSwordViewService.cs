using Assets.Scripts.Paths;

public class PlayerSwordViewService : View
{
    private PlayerSwordService _sword;

    protected override void Awake()
    {
        base.Awake();
        _sword = GetComponentInParent<PlayerSwordService>();
    }

    private void Start()
    {
        _sword.OnSwing += _sword_OnSwing;
    }

    private void OnDestroy()
    {
        _sword.OnSwing -= _sword_OnSwing;
    }

    private void _sword_OnSwing(object sender, System.EventArgs e) => _animator.SetTrigger(AnimatorParameters.ATTACK);

    public void OnAttackAnimationExit() => _sword.TurnOnCollider(false);
}
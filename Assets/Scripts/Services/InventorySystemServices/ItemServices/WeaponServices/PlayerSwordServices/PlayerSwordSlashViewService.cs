using Assets.Scripts.Constans.Paths;

public class PlayerSwordSlashViewService : View
{
    private PlayerSwordService _sword;

    protected override void Awake()
    {
        base.Awake();
        _sword = GetComponentInParent<PlayerSwordService>();
    }

    private void Start()
    {
        _sword.OnSwing += Sword_OnSwing;
    }

    private void OnDestroy()
    {
        _sword.OnSwing -= Sword_OnSwing;
    }

    private void Sword_OnSwing(object sender, System.EventArgs e) => _animator.SetTrigger(AnimatorParameters.ATTACK);
}
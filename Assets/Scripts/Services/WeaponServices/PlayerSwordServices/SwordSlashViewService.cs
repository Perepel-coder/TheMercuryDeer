using Assets.Scripts.Paths;

public class SwordSlashViewService : View
{
    private SwordService _sword;

    protected override void Awake()
    {
        base.Awake();
        _sword = GetComponentInParent<SwordService>();
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
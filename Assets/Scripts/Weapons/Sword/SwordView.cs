using TheMercuryDeer.Scripts.Utils;

public class SwordView : View
{
    private Sword _sword;

    protected override void Awake()
    {
        base.Awake();
        _sword = GetComponentInParent<Sword>();
    }

    private void Start()
    {
        _sword.OnSwing += Sword_OnSwing;
    }

    private void Sword_OnSwing(object sender, System.EventArgs e) => _animator.SetTrigger(Utils.ATTACK);

    public void OnAttackAnimationExit() => _sword.TurnOnCollider(false);
}
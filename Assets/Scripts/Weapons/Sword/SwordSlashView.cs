using UnityEngine;

public class SwordSlashView : View
{
    private const string IS_ATTACK = "isAttack";
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

    private void Sword_OnSwing(object sender, System.EventArgs e) => _animator.SetTrigger(IS_ATTACK);
}
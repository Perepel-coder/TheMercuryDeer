using Assets.Scripts.Paths;
using Assets.Scripts.Player;
using Assets.Scripts.Tools;

public class PlayerView : View
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        PlayerEntity.Instance.OnDeath += PlayerEntity_OnDeath;
    }

    private void PlayerEntity_OnDeath(object sender, System.EventArgs e) => _animator.SetTrigger(AnimatorParameters.DEATH);

    private void Update()
    {
        _animator.SetBool(AnimatorParameters.IS_RUNNING_FORWARD, Player.Instance.IsRunningForward);
        _animator.SetBool(AnimatorParameters.IS_RUNNING_SIDE, Player.Instance.IsRunningSide);
        _animator.SetBool(AnimatorParameters.IS_ATTACKING, Player.Instance.IsAttacking);

        if(PlayerEntity.Instance.IsAlive)
            AdjustPlayerFacingDirection();
    }

    private void LookWhereGoing()
    {
        if(Player.Instance.MovementVector.x < 0)
            _spriteRenderer.flipX = true;
        else if(Player.Instance.MovementVector.x > 0)
            _spriteRenderer.flipX = false;
    }

    private void AdjustPlayerFacingDirection() => _spriteRenderer.flipX = GameInput.Instance.MousePosition.x > Player.Instance.ScreenPosition.x;
}

using TheMercuryDeer.Scripts.Utils;

public class PlayerView : View
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        _animator.SetBool(Utils.IS_RUNNING, Player.Instance.IsRunning);

        //LookWhereGoing();

        AdjustPlayerFacingDirection();
    }

    private void LookWhereGoing()
    {
        if(Player.Instance.MovementVector.x < 0)
            _spriteRenderer.flipX = true;
        else if(Player.Instance.MovementVector.x > 0)
            _spriteRenderer.flipX = false;
    }
    private void AdjustPlayerFacingDirection() => _spriteRenderer.flipX = GameInput.Instance.MousePosition.x < Player.Instance.ScreenPosition.x;
}

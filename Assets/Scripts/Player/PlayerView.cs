using Assets.Scripts.Paths;
using Assets.Scripts.Player;

namespace Assets.Scripts.Player
{
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
            _animator.SetBool(AnimatorParameters.IS_RUNNING_FORWARD, global::Player.Instance.IsRunningForward);
            _animator.SetBool(AnimatorParameters.IS_RUNNING_SIDE, global::Player.Instance.IsRunningSide);
            _animator.SetBool(AnimatorParameters.IS_ATTACKING, global::Player.Instance.IsAttacking);

            if (PlayerEntity.Instance.IsAlive)
                AdjustPlayerFacingDirection();
        }

        private void LookWhereGoing()
        {
            if (global::Player.Instance.MovementVector.x < 0)
                _spriteRenderer.flipX = true;
            else if (global::Player.Instance.MovementVector.x > 0)
                _spriteRenderer.flipX = false;
        }

        private void AdjustPlayerFacingDirection() => _spriteRenderer.flipX = GameInput.Instance.MousePosition.x > global::Player.Instance.ScreenPosition.x;
    }
}

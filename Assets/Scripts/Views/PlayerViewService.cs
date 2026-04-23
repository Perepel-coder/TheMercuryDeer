using Assets.InputActions;
using Assets.Scripts.Constants.Paths;
using Assets.Scripts.Services.Player;
using UnityEngine;

namespace Assets.Scripts.Views
{
    public class PlayerViewService : View
    {
        private TrailRenderer _traceOfDash;

        protected override void Awake()
        {
            base.Awake();
            _traceOfDash = GetComponentInChildren<TrailRenderer>();
        }

        private void Start()
        {
            PlayerEntityService.Instance.OnDeath += PlayerEntity_OnDeath;
        }

        private void OnDestroy()
        {
            PlayerEntityService.Instance.OnDeath -= PlayerEntity_OnDeath;
        }

        private void PlayerEntity_OnDeath(object sender, System.EventArgs e) => _animator.SetTrigger(AnimatorParameters.DEATH);

        private void Update()
        {
            if (PlayerEntityService.Instance.IsAlive)
            {
                AdjustPlayerFacingDirection();

                _animator.SetBool(AnimatorParameters.IS_RUNNING_FORWARD, PlayerService.Instance.IsRunningForward);
                _animator.SetBool(AnimatorParameters.IS_RUNNING_SIDE, PlayerService.Instance.IsRunningSide);
                _animator.SetFloat(AnimatorParameters.SIDE_RUN_SPEED, GetSideRunSpeed());
                _animator.SetBool(AnimatorParameters.IS_ATTACKING, PlayerService.Instance.IsAttacking);

                _traceOfDash.emitting = PlayerService.Instance.IsDashing;
            }    
        }

        private void LookWhereGoing()
        {
            if (PlayerService.Instance.MovementVector.x < 0)
                _spriteRenderer.flipX = true;
            else if (PlayerService.Instance.MovementVector.x > 0)
                _spriteRenderer.flipX = false;
        }

        private float GetSideRunSpeed() => _spriteRenderer.flipX != PlayerService.Instance.MovementVector.x > 0 ? -1f : 1f;

        private void AdjustPlayerFacingDirection() => _spriteRenderer.flipX = GameInput.Instance.MousePosition.x > PlayerService.Instance.ScreenPosition.x;
    }
}

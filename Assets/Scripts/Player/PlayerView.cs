using Assets.Scripts.Paths;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerView : View
    {
        private TrailRenderer _traceOfDash;

        protected override void Awake()
        {
            base.Awake();
            _traceOfDash = GetComponentInChildren<TrailRenderer>();
        }

        private void Start()
        {
            PlayerEntity.Instance.OnDeath += PlayerEntity_OnDeath;
        }

        private void OnDestroy()
        {
            PlayerEntity.Instance.OnDeath -= PlayerEntity_OnDeath;
        }

        private void PlayerEntity_OnDeath(object sender, System.EventArgs e) => _animator.SetTrigger(AnimatorParameters.DEATH);

        private void Update()
        {
            if (PlayerEntity.Instance.IsAlive)
            {
                AdjustPlayerFacingDirection();

                _animator.SetBool(AnimatorParameters.IS_RUNNING_FORWARD, global::Player.Instance.IsRunningForward);
                _animator.SetBool(AnimatorParameters.IS_RUNNING_SIDE, global::Player.Instance.IsRunningSide);
                _animator.SetFloat(AnimatorParameters.SIDE_RUN_SPEED, GetSideRunSpeed());
                _animator.SetBool(AnimatorParameters.IS_ATTACKING, global::Player.Instance.IsAttacking);

                _traceOfDash.emitting = global::Player.Instance.IsDashing;
            }    
        }

        private void LookWhereGoing()
        {
            if (global::Player.Instance.MovementVector.x < 0)
                _spriteRenderer.flipX = true;
            else if (global::Player.Instance.MovementVector.x > 0)
                _spriteRenderer.flipX = false;
        }

        private float GetSideRunSpeed() => _spriteRenderer.flipX != global::Player.Instance.MovementVector.x > 0 ? -1f : 1f;

        private void AdjustPlayerFacingDirection() => _spriteRenderer.flipX = GameInput.Instance.MousePosition.x > global::Player.Instance.ScreenPosition.x;
    }
}

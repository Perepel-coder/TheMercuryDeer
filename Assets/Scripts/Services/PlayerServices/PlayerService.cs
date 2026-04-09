using Assets.InputActions;
using Assets.Scripts.Application.Interfaces.Entity;
using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Services.Player
{
    [SelectionBase]
    public partial class PlayerService
    {
        private PlayerDTO _stats;
        private ActiveWeaponService _activeWeapon;
        private Rigidbody2D _rigidbody;
    }
    public partial class PlayerService : MonoBehaviour, IHasHealth
    {
        public static PlayerService Instance { get; private set; }

        public bool CanDash = true;

        public float CurrentHealth { get; set; }
        public float MaxHealth { get; private set; }

        public bool IsRunningForward { get; private set; }
        public bool IsRunningSide { get; private set; }
        public bool IsAttacking => _activeWeapon.Weapon.IsAttacking;

        public Vector3 ScreenPosition => Camera.main.WorldToScreenPoint(transform.position);

        public Vector2 MovementVector { get; private set; }


        private void Awake()
        {
            Instance = this;
            IsRunningForward = false;
            IsRunningSide = false;

            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _stats = DatabaseService.PlayerRepository.GetPlayers().FirstOrDefault();

            if(_stats == null)
            {
                Debug.LogError("No player stats found in database!");
                return;
            }

            MaxHealth = _stats.MaxHealth;

            _stats.SpeedCurrrentMoveing = _stats.BaseSpeedMoveing;

            _activeWeapon = GetComponentInChildren<ActiveWeaponService>();
            _activeWeapon.UseFollowMousePosition = true;

            GameInput.Instance.OnPlayerDash += GameInput_OnPlayerDash;
            GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
            PlayerEntityService.Instance.OnDeath += _ownerEntity_OnDeath;
        }

        private void OnDestroy()
        {
            GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
            PlayerEntityService.Instance.OnDeath -= _ownerEntity_OnDeath;
        }

        private void Update() => MovementVector = GameInput.Instance.PlayerMovementVector;

        private void FixedUpdate()
        {
            if (PlayerEntityService.Instance.IsAlive)
                HandleMovement();
        }

        private void _ownerEntity_OnDeath(object sender, EventArgs e)
        {
            //TODO: add death logic
            foreach (var collider in GetComponents<Collider2D>())
                collider.enabled = false;

            foreach (var c in GetComponentsInChildren<Collider2D>())
                c.enabled = false;

            _stats.SpeedCurrrentMoveing = _stats.SpeedMoveingMin;

            _activeWeapon.UseFollowMousePosition = false;

            OnDestroy();
        }

        private void HandleMovement()
        {
            _rigidbody.MovePosition(_rigidbody.position + MovementVector * (Time.fixedDeltaTime * _stats.SpeedCurrrentMoveing));

            IsRunningForward = Math.Abs(MovementVector.y) > _stats.SpeedMoveingMin;
            IsRunningSide = Math.Abs(MovementVector.x) > _stats.SpeedMoveingMin;
        }

        private void GameInput_OnPlayerAttack(object sender, EventArgs args) => _activeWeapon.Weapon.Attack();

        private void GameInput_OnPlayerDash(object sender, EventArgs e)
        {
            if (CanDash)
                DashRoutine().Forget();
        }

        private async UniTaskVoid DashRoutine()
        {
            CanDash = false;
            _stats.SpeedCurrrentMoveing *= _stats.DashSpeedMultiplier;
            await UniTask.Delay(TimeSpan.FromSeconds(_stats.DashDuration));

            _stats.SpeedCurrrentMoveing = _stats.BaseSpeedMoveing;
            await UniTask.Delay(TimeSpan.FromSeconds(_stats.DashCooldown));
            CanDash = true;
        }
    }
}
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.InputActions
{

    public class GameInput : MonoBehaviour
    {
        private global::InputActions _inputActions;

        public static GameInput Instance { get; private set; }
        public Vector2 PlayerMovementVector => _inputActions.Player.Move.ReadValue<Vector2>();
        public Vector3 MousePosition => Mouse.current.position.ReadValue();

        public event EventHandler OnPlayerAttack;
        public event EventHandler OnPlayerDash;
        public event EventHandler OnPlayerOpenInventory;
        public event EventHandler OnPlayerInteractWithItem;

        private void Awake()
        {
            Instance = this;

            _inputActions = new();
            _inputActions.Enable();

            _inputActions.Combat.Attack.started += PlayerAttackStarted;
            _inputActions.Player.Dash.performed += PlayerDashPerformed;
            _inputActions.Inventory.Open.started += PlayerOpenInventoryStarted;
            _inputActions.Player.Interact.started += PlayerInteractStarted;
        }

        private void OnDestroy()
        {
            _inputActions.Combat.Attack.started -= PlayerAttackStarted;
            _inputActions.Player.Dash.performed -= PlayerDashPerformed;
            _inputActions.Inventory.Open.started -= PlayerOpenInventoryStarted;
            _inputActions.Player.Interact.started -= PlayerInteractStarted;
        }

        private void PlayerAttackStarted(InputAction.CallbackContext obj)
        {
            OnPlayerAttack?.Invoke(this, EventArgs.Empty);
        }

        private void PlayerDashPerformed(InputAction.CallbackContext obj)
        {
            OnPlayerDash?.Invoke(this, EventArgs.Empty);
        }

        private void PlayerOpenInventoryStarted(InputAction.CallbackContext obj)
        {
            OnPlayerOpenInventory?.Invoke(this, EventArgs.Empty);
        }

        private void PlayerInteractStarted(InputAction.CallbackContext obj)
        {
            OnPlayerInteractWithItem?.Invoke(this, EventArgs.Empty);
        }

        public void SetCombatEnabled(bool enabled)
        {
            if (enabled)
                _inputActions.Combat.Enable();
            else
                _inputActions.Combat.Disable();
        }
    }
}

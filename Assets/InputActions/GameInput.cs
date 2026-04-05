using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private InputActions _inputActions;

    public static GameInput Instance { get; private set; }
    public Vector2 PlayerMovementVector => _inputActions.Player.Move.ReadValue<Vector2>();
    public Vector3 MousePosition => Mouse.current.position.ReadValue();

    public event EventHandler? OnPlayerAttack;
    public event EventHandler? OnPlayerDash;
    public event EventHandler? OnPlayerOpenInventory;

    private void Awake()
    {
        Instance = this;

        _inputActions = new();
        _inputActions.Enable();

        _inputActions.Combat.Attack.started += PlayerAttackStarted;
        _inputActions.Player.Dash.performed += PlayerDashPerformed;
        _inputActions.Inventory.Open.started += PlayerOpenInventoryStarted;
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
}

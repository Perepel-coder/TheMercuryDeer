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

    private void Awake()
    {
        Instance = this;

        _inputActions = new();
        _inputActions.Enable();

        _inputActions.Combat.Attack.started += PalyerAttackStarted;
        _inputActions.Player.Dash.performed += PlayerDashPerformed;
    }

    private void PalyerAttackStarted(InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerDashPerformed(InputAction.CallbackContext obj)
    {
        OnPlayerDash?.Invoke(this, EventArgs.Empty);
    }
}

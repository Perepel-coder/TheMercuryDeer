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

    private void Awake()
    {
        Instance = this;

        _inputActions = new();
        _inputActions.Enable();

        _inputActions.Combat.Attack.started += PalyerAttackStarted;
    }

    private void PalyerAttackStarted(InputAction.CallbackContext obj)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }
}

using System;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _speedMoveing = 3f;

    private float _speedMoveingMin = 0.1f;

    public static Player Instance { get; private set; }
    public bool IsRunning { get; private set; }
    public Vector2 MovementVector { get; private set; }

    public Vector3 ScreenPosition => Camera.main.WorldToScreenPoint(transform.position);

    private void Awake()
    {
        Instance = this;
        IsRunning = false;

        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
    }

    private void Update()
    {
        MovementVector = GameInput.Instance.PlayerMovementVector;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _rigidbody.MovePosition(_rigidbody.position + MovementVector * (Time.fixedDeltaTime * _speedMoveing));

        IsRunning = Math.Abs(MovementVector.x) > _speedMoveingMin || Math.Abs(MovementVector.y) > _speedMoveingMin;
    }

    private void GameInput_OnPlayerAttack(object sender, EventArgs args) => ActiveWeapon.Instance.Weapon.Attack();
}

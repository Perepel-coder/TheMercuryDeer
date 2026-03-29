using Assets.Scripts.Interfaces.Entity;
using System;
using UnityEngine;

[SelectionBase]
public partial class Player
{
    private Rigidbody2D _rigidbody;

    #region characteristics
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; private set; } = 100;

    [SerializeField] private float _speedMoveing = 2f;
    private float _speedMoveingMin = 0.1f;
    #endregion

    #region inventory
    private ActiveWeapon _activeWeapon;
    #endregion
}
public partial class Player : MonoBehaviour, IHasHealth
{
    public bool IsRunning { get; private set; }
    public Vector3 ScreenPosition => Camera.main.WorldToScreenPoint(transform.position);

    public static Player Instance { get; private set; }
    public Vector2 MovementVector { get; private set; }


    private void Awake()
    {
        Instance = this;
        IsRunning = false;

        _rigidbody = GetComponent<Rigidbody2D>();
        _activeWeapon = GetComponentInChildren<ActiveWeapon>();
    }

    private void Start()
    {
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        _activeWeapon.UseFollowMousePosition = true;
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
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

    private void GameInput_OnPlayerAttack(object sender, EventArgs args) => _activeWeapon.Weapon.Attack();
}

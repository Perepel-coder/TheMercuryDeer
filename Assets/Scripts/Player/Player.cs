using Assets.Scripts.Interfaces.Entity;
using Assets.Scripts.Player;
using System;
using UnityEngine;

[SelectionBase]
public partial class Player
{
    private Rigidbody2D _rigidbody;

    #region characteristics
    public int CurrentHealth { get; set; }
    public int MaxHealth { get; private set; } = 100;

    [SerializeField] private float _speedMoveing = 1f;
    private float _speedMoveingMin = 0.1f;
    #endregion

    #region inventory
    private ActiveWeapon _activeWeapon;
    #endregion
}
public partial class Player : MonoBehaviour, IHasHealth
{
    public bool IsRunningForward { get; private set; }
    public bool IsRunningSide { get; private set; }
    public bool IsAttacking => _activeWeapon.Weapon.IsAttacking;
    public Vector3 ScreenPosition => Camera.main.WorldToScreenPoint(transform.position);

    public static Player Instance { get; private set; }
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
        _activeWeapon = GetComponentInChildren<ActiveWeapon>();

        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        PlayerEntity.Instance.OnDeath += _ownerEntity_OnDeath;

        _activeWeapon.UseFollowMousePosition = true;
    }

    private void _ownerEntity_OnDeath(object sender, EventArgs e)
    {
        //TODO: add death logic
        foreach (var collider in GetComponents<Collider2D>()) 
            collider.enabled = false;

        foreach (var c in GetComponentsInChildren<Collider2D>())
            c.enabled = false;

        _speedMoveing = _speedMoveingMin;

        _activeWeapon.UseFollowMousePosition = false;

        OnDestroy();
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnPlayerAttack -= GameInput_OnPlayerAttack;
        PlayerEntity.Instance.OnDeath -= _ownerEntity_OnDeath;
    }

    private void Update()
    {
        MovementVector = GameInput.Instance.PlayerMovementVector;
    }

    private void FixedUpdate()
    {
        if(PlayerEntity.Instance.IsAlive)
            HandleMovement();
    }

    private void HandleMovement()
    {
        _rigidbody.MovePosition(_rigidbody.position + MovementVector * (Time.fixedDeltaTime * _speedMoveing));

        IsRunningForward = Math.Abs(MovementVector.y) > _speedMoveingMin;
        IsRunningSide = Math.Abs(MovementVector.x) > _speedMoveingMin;
    }

    private void GameInput_OnPlayerAttack(object sender, EventArgs args) => _activeWeapon.Weapon.Attack();
}

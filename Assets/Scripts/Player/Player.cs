using Assets.Scripts.Interfaces.Npc;
using System;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _speedMoveing = 2f;
    private float _speedMoveingMin = 0.1f;
    private ActiveWeapon _activeWeapon;


    public static Player Instance { get; private set; }
    public bool IsRunning { get; private set; }
    public Vector2 MovementVector { get; private set; }

    public Vector3 ScreenPosition => Camera.main.WorldToScreenPoint(transform.position);

    public bool IsAlive => throw new NotImplementedException();

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

    public void TakeDamage(int damage)
    {
        Debug.Log($"Player took {damage} damage");
    }

    public void Die()
    {

    }
}

using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected PolygonCollider2D _attackCollider;
    public abstract int DamageAmount { get; protected set; }

    public abstract void Attack();


    protected virtual void Awake()
    {
        _attackCollider = GetComponent<PolygonCollider2D>();
    }

    protected virtual void Start()
    {
        TurnOnCollider(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out NpcEntity enemyEntity))
            enemyEntity.TakeDamage(DamageAmount);
    }

    public void TurnOnCollider(bool enable) => _attackCollider.enabled = enable;
}
namespace Assets.Scripts.Interfaces.Npc
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
        public bool IsAlive { get; }
        public void Die();
    }
}

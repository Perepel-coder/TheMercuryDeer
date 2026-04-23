using SQLite;

namespace Assets.Scripts.Constants
{
    public static class EnemyDefinitions
    {
        public enum State
        {
            Idle,
            Roaming,
            Chasing,
            Attacking,
            Death
        }

        [StoreAsText]
        public enum EnemyTag
        {
            Amor,
            Seraphim
        }
    }
}

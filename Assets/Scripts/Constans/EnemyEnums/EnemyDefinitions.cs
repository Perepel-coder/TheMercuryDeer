using SQLite;

namespace Assets.Scripts.Enums.EnemyEnums
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

using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using SQLite;
using UnityEditor;
using UnityEngine;
using UnityApplication = UnityEngine.Application;

namespace Assets.Scripts.Editor
{
    public static class DatabaseSeeder
    {
        private static string _dbPath => System.IO.Path.Combine(UnityApplication.persistentDataPath, "game.db");

        private static bool TableExists<T>(SQLiteConnection connection)
        {
            var tableName = typeof(T).Name;
            return connection.GetTableInfo(tableName).Count > 0;
        }

        [MenuItem("Database/Seed Default Player")]
        public static void SeedDefaultPlayer()
        {
            using var connection = new SQLiteConnection(_dbPath);

            connection.CreateTable<Player>();
            connection.DeleteAll<Player>();

            connection.Insert(new Player
            {
                MaxHealth = 100,
                BaseSpeedMoveing = 3f,
                SpeedMoveingMin = 0.1f,
                DashSpeedMultiplier = 4,
                DashDuration = 0.2f,
                DashCooldown = 2f
            });

            Debug.Log($"[DatabaseSeeder] Player seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed Default Enemies")]
        public static void SeedDefaultEnemies()
        {
            using var connection = new SQLiteConnection(_dbPath);

            connection.CreateTable<Enemy>();
            connection.DeleteAll<Enemy>();

            connection.Insert(new Enemy
            {
                Tag = EnemyTag.Amor,
                AttackingDistance = 2.5f,
                InherentDamage = 5,
                MaxHealth = 50
            });

            Debug.Log($"[DatabaseSeeder] Enemies seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed Default Weapons")]
        public static void SeeddDefaultWeapons()
        {
            using var connection = new SQLiteConnection(_dbPath);

            connection.CreateTable<Weapon>();
            connection.DeleteAll<Weapon>();

            connection.Insert(new Weapon
            {
                Tag = WeaponTag.PlayerSword,
                DamageAmount = 1,
                HealthAmount = 0,
                IsContinuousDamage = false,
                DropHeight = 0f,
                PlayerId = 1,
                EnemyId = -1
            });

            connection.Insert(new Weapon
            {
                Tag = WeaponTag.AmorSword,
                DamageAmount = 10,
                HealthAmount = 0,
                IsContinuousDamage = false,
                DropHeight = 3f,
                PlayerId = -1,
                EnemyId = 1
            });

            connection.Insert(new Weapon
            {
                Tag = WeaponTag.BaseReactionToTakingHit,
                DamageAmount = 0,
                HealthAmount = 0,
                IsContinuousDamage = false,
                DropHeight = 0f,
                PlayerId = -1,
                EnemyId = 1
            });

            Debug.Log($"[DatabaseSeeder] Weapons seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed All")]
        public static void SeedAll()
        {
            SeedDefaultPlayer();
            SeedDefaultEnemies();
            SeeddDefaultWeapons();
        }

        [MenuItem("Database/Clear All")]
        public static void ClearAll()
        {
            using var connection = new SQLiteConnection(_dbPath);

            if (TableExists<Player>(connection))
                connection.DeleteAll<Player>();

            if (TableExists<Enemy>(connection))
                connection.DeleteAll<Enemy>();

            if (TableExists<Weapon>(connection))
                connection.DeleteAll<Weapon>();

            Debug.Log($"[DatabaseSeeder] All tables cleared. DB path: {_dbPath}");
        }

        [MenuItem("Database/Show DB Path")]
        public static void ShowDbPath()
        {
            Debug.Log($"[DatabaseSeeder] DB path: {_dbPath}");
        }
    }
}

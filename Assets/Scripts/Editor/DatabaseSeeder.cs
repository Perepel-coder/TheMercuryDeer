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

        [MenuItem("Database/Clear All")]
        public static void ClearAll()
        {
            using var connection = new SQLiteConnection(_dbPath);

            if (!TableExists<Player>(connection))
            {
                Debug.LogWarning("[DatabaseSeeder] Table 'Player' not found.");
                return;
            }

            connection.DeleteAll<Player>();

            Debug.Log($"[DatabaseSeeder] All tables cleared. DB path: {_dbPath}");
        }

        [MenuItem("Database/Show DB Path")]
        public static void ShowDbPath()
        {
            Debug.Log($"[DatabaseSeeder] DB path: {_dbPath}");
        }
    }
}

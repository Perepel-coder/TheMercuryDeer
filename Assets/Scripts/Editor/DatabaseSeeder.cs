using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure.Repositories;
using Assets.Scripts.Models;
using SQLite;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using static Assets.Scripts.Enums.EnemyEnums.EnemyDefinitions;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;
using UnityApplication = UnityEngine.Application;

namespace Assets.Scripts.Editor
{
    public static class DatabaseSeeder
    {
        private static string _dbPath => System.IO.Path.Combine(UnityApplication.persistentDataPath, "game.db");

        private static SQLiteAsyncConnection CreateConnection() => new SQLiteAsyncConnection(_dbPath);

        [MenuItem("Database/Seed Default Player")]
        public static void SeedDefaultPlayer()
        {
            var connection = CreateConnection();
            var playerRepo = new PlayerRepository(connection);
            var weaponRepo = new WeaponRepository(connection);

            Task.Run(async () =>
            {
                await connection.CreateTableAsync<Player>();
                await connection.CreateTableAsync<Weapon>();
            }).GetAwaiter().GetResult();

            int id = playerRepo.AddPlayer(new PlayerDTO
            {
                MaxHealth = 100,
                BaseSpeedMoveing = 3f,
                SpeedMoveingMin = 0.1f,
                DashSpeedMultiplier = 4,
                DashDuration = 0.2f,
                DashCooldown = 0.5f
            });

            weaponRepo.AddWeapon(new WeaponDTO { Tag = Tag.PlayerSword, DamageAmount = 1, PlayerId = id });

            Task.Run(() => connection.CloseAsync()).GetAwaiter().GetResult();
            Debug.Log($"[DatabaseSeeder] Player seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed Default Enemies")]
        public static void SeedDefaultEnemies()
        {
            var connection = CreateConnection();
            var playerRepo = new EnemyRepository(connection);
            var weaponRepo = new WeaponRepository(connection);

            Task.Run(async () =>
            {
                await connection.CreateTableAsync<Enemy>();
                await connection.CreateTableAsync<Weapon>();
            }).GetAwaiter().GetResult();

            int id = playerRepo.AddEnemy(new EnemyDTO
            {
                Tag = EnemyTag.Amor,
                AttackingDistance = 2.5f,
                InherentDamage = 5,
                MaxHealth = 50
            });

            weaponRepo.AddWeapon(new WeaponDTO { Tag = Tag.AmorSword, DamageAmount = 10, DropHeight = 3f, EnemyId = id });
            weaponRepo.AddWeapon(new WeaponDTO { Tag = Tag.BaseReactionToTakingHit, EnemyId = id });

            Task.Run(() => connection.CloseAsync()).GetAwaiter().GetResult();
            Debug.Log($"[DatabaseSeeder] Enemies seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed All")]
        public static void SeedAll()
        {
            SeedDefaultPlayer();
            SeedDefaultEnemies();
        }

        [MenuItem("Database/Clear All")]
        public static void ClearAll()
        {
            var connection = CreateConnection();
            Task.Run(() => connection.CloseAsync()).GetAwaiter().GetResult();

            if (System.IO.File.Exists(_dbPath))
            {
                System.IO.File.Delete(_dbPath);
                Debug.Log($"[DatabaseSeeder] Database deleted. DB path: {_dbPath}");
            }
            else
            {
                Debug.Log($"[DatabaseSeeder] Database file not found. DB path: {_dbPath}");
            }
        }

        [MenuItem("Database/Show DB Path")]
        public static void ShowDbPath() => Debug.Log($"[DatabaseSeeder] DB path: {_dbPath}");
    }
}

using Assets.Scripts.Application.Repositories;
using Assets.Scripts.DTO;
using Assets.Scripts.Models;
using SQLite;
using System.Collections.Generic;
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
            var repo = new PlayerRepository(connection);

            Task.Run(async () =>
            {
                await connection.CreateTableAsync<Player>();
                await connection.DeleteAllAsync<Player>();
            }).GetAwaiter().GetResult();

            repo.AddPlayer(new PlayerDTO
            {
                MaxHealth = 100,
                BaseSpeedMoveing = 3f,
                SpeedMoveingMin = 0.1f,
                DashSpeedMultiplier = 4,
                DashDuration = 0.2f,
                DashCooldown = 1f
            });

            Task.Run(() => connection.CloseAsync()).GetAwaiter().GetResult();
            Debug.Log($"[DatabaseSeeder] Player seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed Default Enemies")]
        public static void SeedDefaultEnemies()
        {
            var connection = CreateConnection();
            var repo = new EnemyRepository(connection);

            Task.Run(async () =>
            {
                await connection.CreateTableAsync<Enemy>();
                await connection.DeleteAllAsync<Enemy>();
            }).GetAwaiter().GetResult();

            repo.AddEnemy(new EnemyDTO
            {
                Tag = EnemyTag.Amor,
                AttackingDistance = 2.5f,
                InherentDamage = 5,
                MaxHealth = 50
            });

            Task.Run(() => connection.CloseAsync()).GetAwaiter().GetResult();
            Debug.Log($"[DatabaseSeeder] Enemies seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed Default Weapons")]
        public static void SeedDefaultWeapons()
        {
            var connection = CreateConnection();
            var repo = new WeaponRepository(connection);

            Task.Run(async () =>
            {
                await connection.CreateTableAsync<Weapon>();
                await connection.DeleteAllAsync<Weapon>();
            }).GetAwaiter().GetResult();

            repo.AddWeapon(new WeaponDTO { Tag = Tag.PlayerSword, DamageAmount = 1, PlayerId = 1 });
            repo.AddWeapon(new WeaponDTO { Tag = Tag.AmorSword, DamageAmount = 10, DropHeight = 3f, EnemyId = 1 });
            repo.AddWeapon(new WeaponDTO { Tag = Tag.BaseReactionToTakingHit, EnemyId = 1 });

            Task.Run(() => connection.CloseAsync()).GetAwaiter().GetResult();
            Debug.Log($"[DatabaseSeeder] Weapons seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed Default Items")]
        public static void SeedDefaultItems()
        {
            var connection = CreateConnection();
            var repo = new ItemRepository(connection);

            Task.Run(async () =>
            {
                await connection.CreateTableAsync<Item>();
                await connection.CreateTableAsync<ItemCategory>();
                await connection.DeleteAllAsync<ItemCategory>();
                await connection.DeleteAllAsync<Item>();
            }).GetAwaiter().GetResult();

            repo.AddItem(new ItemDTO
            {
                ItemTag = Tag.RedApple,
                Name = "Красное яблоко",
                Description = "Просто красное яблоко",
                Quantity = 1,
                StatToChange = StatToChange.Health,
                StatChangePercent = 10,
                Categories = new List<Category> { Category.Consumable, Category.CraftingMaterial },
            });

            Task.Run(() => connection.CloseAsync()).GetAwaiter().GetResult();
            Debug.Log($"[DatabaseSeeder] Items seeded. DB path: {_dbPath}");
        }

        [MenuItem("Database/Seed All")]
        public static void SeedAll()
        {
            SeedDefaultPlayer();
            SeedDefaultEnemies();
            SeedDefaultWeapons();
            SeedDefaultItems();
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

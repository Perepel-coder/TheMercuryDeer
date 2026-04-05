using Assets.Scripts.Application.Interfaces.Repositories;
using Assets.Scripts.Application.Repositories;
using Assets.Scripts.Models;
using SQLite;
using UnityEngine;
using UnityApplication = UnityEngine.Application;

namespace Assets.Scripts.Infrastructure
{
    public static class DatabaseService
    {
        public static SQLiteAsyncConnection Connection { get; private set; }

        public static IPlayerRepository PlayerRepository { get; private set; }
        public static IEnemyRepository EnemyRepository { get; private set; }
        public static IWeaponRepository WeaponRepository { get; private set; }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            string dbPath = System.IO.Path.Combine(UnityApplication.persistentDataPath, "game.db");

            var syncConnection = new SQLiteConnection(dbPath);
            syncConnection.CreateTable<Player>();
            syncConnection.CreateTable<Enemy>();
            syncConnection.CreateTable<Weapon>();
            syncConnection.Close();

            Connection = new SQLiteAsyncConnection(dbPath);
            PlayerRepository = new PlayerRepository(Connection);
            EnemyRepository = new EnemyRepository(Connection);
            WeaponRepository = new WeaponRepository(Connection);

            UnityApplication.quitting += OnApplicationQuitting;
        }

        private static async void OnApplicationQuitting()
        {
            if (Connection != null)
                await Connection.CloseAsync();
        }
    }
}
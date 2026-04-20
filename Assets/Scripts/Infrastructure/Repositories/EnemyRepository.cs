using Assets.Scripts.Application.Interfaces.Repositories;
using Assets.Scripts.Application.Mappers;
using Assets.Scripts.DTO;
using Assets.Scripts.Models;
using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Assets.Scripts.Enums.EnemyEnums.EnemyDefinitions;

namespace Assets.Scripts.Infrastructure.Repositories
{
    public class EnemyRepository : IEnemyRepository
    {
        private readonly ISQLiteAsyncConnection _asyncConnection;

        public EnemyRepository(ISQLiteAsyncConnection asyncConnection) => _asyncConnection = asyncConnection;

        public async UniTask<List<EnemyDTO>> GetEnemiesAsync() => (await _asyncConnection.Table<Enemy>().ToListAsync()).ToDTOList();

        public async UniTask<EnemyDTO> GetEnemyAsync(EnemyTag name) => (await _asyncConnection.Table<Enemy>().Where(e => e.Tag == name).FirstOrDefaultAsync()).ToDTO();

        public async UniTask<int> DeleteEnemyAsync(EnemyDTO enemy) => await _asyncConnection.DeleteAsync(enemy.ToModel());

        public async UniTask UpdateEnemyAsync(EnemyDTO enemy)
        {
            if (await _asyncConnection.FindAsync<Enemy>(enemy.Id) != null)
                await _asyncConnection.UpdateAsync(enemy.ToModel());
        }

        public async UniTask<int> AddEnemyAsync(EnemyDTO enemy)
        {
            Enemy enemyModel = enemy.ToModel();
            if (await _asyncConnection.FindAsync<Enemy>(enemy.Id) == null)
                await _asyncConnection.InsertAsync(enemyModel);
            return enemyModel.Id;
        }



        public List<EnemyDTO> GetEnemies() => Task.Run(() => GetEnemiesAsync().AsTask()).GetAwaiter().GetResult();

        public EnemyDTO GetEnemy(EnemyTag name) => Task.Run(() => GetEnemyAsync(name).AsTask()).GetAwaiter().GetResult();

        public int DeleteEnemy(EnemyDTO enemy) => Task.Run(() => DeleteEnemyAsync(enemy).AsTask()).GetAwaiter().GetResult();

        public void UpdateEnemy(EnemyDTO enemy) => Task.Run(() => UpdateEnemyAsync(enemy).AsTask()).GetAwaiter().GetResult();
        public int AddEnemy(EnemyDTO enemy) => Task.Run(() => AddEnemyAsync(enemy).AsTask()).GetAwaiter().GetResult();
    }
}

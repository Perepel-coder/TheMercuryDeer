using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.Scripts.Application.Interfaces.Repositories
{
    public interface IEnemyRepository
    {
        public UniTask<List<EnemyDTO>> GetEnemiesAsync();

        public UniTask<EnemyDTO> GetEnemyAsync(EnemyName name);

        public UniTask<int> DeleteEnemyAsync(EnemyDTO enemy);

        public UniTask UpdateEnemyAsync(EnemyDTO enemy);

        public UniTask<int> AddEnemyAsync(EnemyDTO enemy);

        public List<EnemyDTO> GetEnemies();

        public EnemyDTO GetEnemy(EnemyName name);

        public int DeleteEnemy(EnemyDTO enemy);

        public void UpdateEnemy(EnemyDTO enemy);
        public int AddEnemy(EnemyDTO enemy);
    }
}

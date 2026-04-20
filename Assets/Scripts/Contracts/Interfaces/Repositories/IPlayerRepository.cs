using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.Scripts.Application.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        public UniTask<List<PlayerDTO>> GetPlayersAsync();
        public List<PlayerDTO> GetPlayers();

        public UniTask<PlayerDTO> GetPlayerAsync(int id);
        public PlayerDTO GetPlayer(int id);

        public UniTask<int> DeletePlayerAsync(PlayerDTO player);
        public int DeletePlayer(PlayerDTO player);

        public UniTask UpdatePlayerAsync(PlayerDTO player);
        public void UpdatePlayer(PlayerDTO player);

        public UniTask<int> AddPlayerAsync(PlayerDTO player);
        public int AddPlayer(PlayerDTO player);
    }
}

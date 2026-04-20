using Assets.Scripts.Application.Interfaces.Repositories;
using Assets.Scripts.Application.Mappers;
using Assets.Scripts.DTO;
using Assets.Scripts.Models;
using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly ISQLiteAsyncConnection _asyncConnection;

        public PlayerRepository(ISQLiteAsyncConnection asyncConnection) => _asyncConnection = asyncConnection;


        public async UniTask<List<PlayerDTO>> GetPlayersAsync() => (await _asyncConnection.Table<Player>().ToListAsync()).ToDTOList();

        public async UniTask<PlayerDTO> GetPlayerAsync(int id) => (await _asyncConnection.GetAsync<Player>(id)).ToDTO();

        public async UniTask<int> DeletePlayerAsync(PlayerDTO player) => await _asyncConnection.DeleteAsync(player.ToModel());

        public async UniTask UpdatePlayerAsync(PlayerDTO player)
        {
            if (await _asyncConnection.FindAsync<Player>(player.Id) != null)
                await _asyncConnection.UpdateAsync(player.ToModel());
        }

        public async UniTask<int> AddPlayerAsync(PlayerDTO player)
        {
            Player playerModel = player.ToModel();
            if (await _asyncConnection.FindAsync<Player>(player.Id) == null)
                await _asyncConnection.InsertAsync(playerModel);
            return playerModel.Id;
        }

        public List<PlayerDTO> GetPlayers() => Task.Run(() => GetPlayersAsync().AsTask()).GetAwaiter().GetResult();

        public PlayerDTO GetPlayer(int id) => Task.Run(() => GetPlayerAsync(id).AsTask()).GetAwaiter().GetResult();

        public int DeletePlayer(PlayerDTO player) => Task.Run(() => DeletePlayerAsync(player).AsTask()).GetAwaiter().GetResult();

        public void UpdatePlayer(PlayerDTO player) => Task.Run(() => UpdatePlayerAsync(player).AsTask()).GetAwaiter().GetResult();

        public int AddPlayer(PlayerDTO player) => Task.Run(() => AddPlayerAsync(player).AsTask()).GetAwaiter().GetResult();
    }
}

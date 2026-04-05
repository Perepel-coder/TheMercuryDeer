using Assets.Scripts.Application.Interfaces.Repositories;
using Assets.Scripts.Application.Mappers;
using Assets.Scripts.DTO;
using Assets.Scripts.Enums;
using Assets.Scripts.Models;
using Cysharp.Threading.Tasks;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Application.Repositories
{
    public class WeaponRepository : IWeaponRepository
    {
        private readonly ISQLiteAsyncConnection _asyncConnection;

        public WeaponRepository(ISQLiteAsyncConnection asyncConnection) => _asyncConnection = asyncConnection;

        public async UniTask<List<WeaponDTO>> GetWeaponsAsync() => (await _asyncConnection.Table<Weapon>().ToListAsync()).ToDTOList();

        public async UniTask<WeaponDTO> GetWeaponByTagAsync(WeaponTag tag) => (await _asyncConnection.Table<Weapon>().Where(w => w.Tag == tag).FirstOrDefaultAsync()).ToDTO();

        public async UniTask<WeaponDTO> GetWeaponAsync(int id) => (await _asyncConnection.GetAsync<Weapon>(id)).ToDTO();

        public async UniTask<List<WeaponDTO>> GetPlayerWeaponsAsync(int playerId) => (await _asyncConnection.Table<Weapon>().Where(w => w.PlayerId == playerId).ToListAsync()).ToDTOList();

        public async UniTask<List<WeaponDTO>> GetEnemyWeaponsAsync(int enemyId) => (await _asyncConnection.Table<Weapon>().Where(w => w.EnemyId == enemyId).ToListAsync()).ToDTOList();

        public async UniTask<int> DeleteWeaponAsync(WeaponDTO weapon) => await _asyncConnection.DeleteAsync(weapon.ToModel());

        public async UniTask UpdateWeaponAsync(WeaponDTO weapon)
        {
            if (await _asyncConnection.FindAsync<Weapon>(weapon.Id) != null)
                await _asyncConnection.UpdateAsync(weapon.ToModel());
        }

        public async UniTask<int> AddWeaponAsync(WeaponDTO weapon)
        {
            Weapon weaponModel = weapon.ToModel();
            if (await _asyncConnection.FindAsync<Weapon>(weapon.Id) == null)
                await _asyncConnection.InsertAsync(weaponModel);
            return weaponModel.Id;
        }

        public List<WeaponDTO> GetWeapons() => Task.Run(() => GetWeaponsAsync().AsTask()).GetAwaiter().GetResult();

        public WeaponDTO GetWeaponByTag(WeaponTag tag) => Task.Run(() => GetWeaponByTagAsync(tag).AsTask()).GetAwaiter().GetResult();

        public WeaponDTO GetWeapon(int id) => Task.Run(() => GetWeaponAsync(id).AsTask()).GetAwaiter().GetResult();

        public List<WeaponDTO> GetPlayerWeapons(int playerId) => Task.Run(() => GetPlayerWeaponsAsync(playerId).AsTask()).GetAwaiter().GetResult();

        public List<WeaponDTO> GetEnemyWeapons(int enemyId) => Task.Run(() => GetEnemyWeaponsAsync(enemyId).AsTask()).GetAwaiter().GetResult();

        public int DeleteWeapon(WeaponDTO weapon) => Task.Run(() => DeleteWeaponAsync(weapon).AsTask()).GetAwaiter().GetResult();

        public void UpdateWeapon(WeaponDTO weapon) => Task.Run(() => UpdateWeaponAsync(weapon).AsTask()).GetAwaiter().GetResult();

        public int AddWeapon(WeaponDTO weapon) => Task.Run(() => AddWeaponAsync(weapon).AsTask()).GetAwaiter().GetResult();
    }
}

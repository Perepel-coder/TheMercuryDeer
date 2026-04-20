using Assets.Scripts.DTO;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Application.Interfaces.Repositories
{
    public interface IWeaponRepository
    {
        UniTask<List<WeaponDTO>> GetWeaponsAsync();
        List<WeaponDTO> GetWeapons();

        UniTask<WeaponDTO> GetWeaponByTagAsync(Tag tag);
        WeaponDTO GetWeaponByTag(Tag tag);

        UniTask<WeaponDTO> GetWeaponAsync(int id);
        WeaponDTO GetWeapon(int id);

        UniTask<List<WeaponDTO>> GetPlayerWeaponsAsync(int playerId);
        List<WeaponDTO> GetPlayerWeapons(int playerId);

        UniTask<List<WeaponDTO>> GetEnemyWeaponsAsync(int enemyId);
        List<WeaponDTO> GetEnemyWeapons(int enemyId);

        UniTask<int> DeleteWeaponAsync(WeaponDTO weapon);
        int DeleteWeapon(WeaponDTO weapon);

        UniTask UpdateWeaponAsync(WeaponDTO weapon);
        void UpdateWeapon(WeaponDTO weapon);

        UniTask<int> AddWeaponAsync(WeaponDTO weapon);
        int AddWeapon(WeaponDTO weapon);
    }
}

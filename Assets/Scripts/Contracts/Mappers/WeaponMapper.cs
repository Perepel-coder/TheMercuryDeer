using Assets.Scripts.DTO;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Application.Mappers
{
    public static class WeaponMapper
    {
        public static WeaponDTO ToDTO(this Weapon weapon) => new WeaponDTO
        {
            Id = weapon.Id,
            Tag = weapon.Tag,
            HealthAmount = weapon.HealthAmount,
            DamageAmount = weapon.DamageAmount,
            IsContinuousDamage = weapon.IsContinuousDamage,
            DropHeight = weapon.DropHeight,
            PlayerId = weapon.PlayerId,
            EnemyId = weapon.EnemyId
        };

        public static Weapon ToModel(this WeaponDTO dto) => new Weapon
        {
            Id = dto.Id,
            Tag = dto.Tag,
            HealthAmount = dto.HealthAmount,
            DamageAmount = dto.DamageAmount,
            IsContinuousDamage = dto.IsContinuousDamage,
            DropHeight = dto.DropHeight,
            PlayerId = dto.PlayerId,
            EnemyId = dto.EnemyId
        };

        public static List<WeaponDTO> ToDTOList(this IEnumerable<Weapon> weapons) =>
            weapons.Select(w => w.ToDTO()).ToList();
    }
}

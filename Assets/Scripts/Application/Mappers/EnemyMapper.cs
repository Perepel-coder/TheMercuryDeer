using Assets.Scripts.DTO;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Application.Mappers
{
    public static class EnemyMapper
    {
        public static EnemyDTO ToDTO(this Enemy enemy) => new EnemyDTO
        {
            Id = enemy.Id,
            Tag = enemy.Tag,
            RoamingDistanceMin = enemy.RoamingDistanceMin,
            RoamingDistanceMax = enemy.RoamingDistanceMax,
            RoamingTimeMax = enemy.RoamingTimeMax,
            RoamingSpeed = enemy.RoamingSpeed,
            ChasingDistance = enemy.ChasingDistance,
            ChasingSpeedMultiplier = enemy.ChasingSpeedMultiplier,
            AttackingDistance = enemy.AttackingDistance,
            AttackRate = enemy.AttackRate,
            InherentDamage = enemy.InherentDamage,
            MaxHealth = enemy.MaxHealth
        };

        public static Enemy ToModel(this EnemyDTO dto) => new Enemy
        {
            Id = dto.Id,
            Tag = dto.Tag,
            RoamingDistanceMin = dto.RoamingDistanceMin,
            RoamingDistanceMax = dto.RoamingDistanceMax,
            RoamingTimeMax = dto.RoamingTimeMax,
            RoamingSpeed = dto.RoamingSpeed,
            ChasingDistance = dto.ChasingDistance,
            ChasingSpeedMultiplier = dto.ChasingSpeedMultiplier,
            AttackingDistance = dto.AttackingDistance,
            AttackRate = dto.AttackRate,
            InherentDamage = dto.InherentDamage,
            MaxHealth = dto.MaxHealth
        };

        public static List<EnemyDTO> ToDTOList(this IEnumerable<Enemy> enemies) =>
            enemies.Select(e => e.ToDTO()).ToList();
    }
}

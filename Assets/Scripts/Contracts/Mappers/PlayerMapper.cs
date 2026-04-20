using Assets.Scripts.DTO;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Application.Mappers
{
    public static class PlayerMapper
    {
        public static PlayerDTO ToDTO(this Player player) => new PlayerDTO
        {
            Id = player.Id,
            MaxHealth = player.MaxHealth,
            BaseSpeedMoveing = player.BaseSpeedMoveing,
            SpeedMoveingMin = player.SpeedMoveingMin,
            DashSpeedMultiplier = player.DashSpeedMultiplier,
            DashDuration = player.DashDuration,
            DashCooldown = player.DashCooldown
        };

        public static Player ToModel(this PlayerDTO dto) => new Player
        {
            Id = dto.Id,
            MaxHealth = dto.MaxHealth,
            BaseSpeedMoveing = dto.BaseSpeedMoveing,
            SpeedMoveingMin = dto.SpeedMoveingMin,
            DashSpeedMultiplier = dto.DashSpeedMultiplier,
            DashDuration = dto.DashDuration,
            DashCooldown = dto.DashCooldown
        };

        public static List<PlayerDTO> ToDTOList(this IEnumerable<Player> players) =>
            players.Select(p => p.ToDTO()).ToList();
    }
}

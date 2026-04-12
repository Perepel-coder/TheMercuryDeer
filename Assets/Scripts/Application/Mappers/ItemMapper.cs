using Assets.Scripts.DTO;
using Assets.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Application.Mappers
{
    public static class ItemMapper
    {
        public static ItemDTO ToDTO(this Item item, IEnumerable<Category> categories = null) => new ItemDTO
        {
            Id = item.Id,
            Name = item.Name,
            ItemTag = item.ItemTag,
            Description = item.Description,
            Quantity = item.Quantity,
            StatToChange = item.StatToChange,
            StatChangePercent = item.StatChangePercent,
            Categories = categories?.ToList() ?? new List<Category>()
        };

        public static Item ToModel(this ItemDTO dto) => new Item
        {
            Id = dto.Id,
            Name = dto.Name,
            ItemTag = dto.ItemTag,
            Description = dto.Description,
            Quantity = dto.Quantity,
            StatToChange = dto.StatToChange,
            StatChangePercent = dto.StatChangePercent
        };

        public static List<ItemDTO> ToDTOList(this IEnumerable<Item> items) =>
            items.Select(i => i.ToDTO()).ToList();
    }
}

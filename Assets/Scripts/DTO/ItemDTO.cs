using System.Collections.Generic;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.DTO
{
    public class ItemDTO
    {
        public int Id { get; }
        public string Name { get; }
        public Tag ItemTag { get; }
        public string Description { get; }
        public StatToChange StatToChange { get; }
        public float StatChangePercent { get; }
        public List<Category> Categories { get; }

        public ItemDTO(
            string name,
            Tag itemTag,
            string description,
            StatToChange statToChange,
            float statChangePercent,
            List<Category> categories)
        {
            Name = name;
            ItemTag = itemTag;
            StatToChange = statToChange;
            StatChangePercent = statChangePercent;
            Categories = categories;
        }

        public ItemDTO(
            int id,
            string name,
            Tag itemTag,
            string description,
            StatToChange statToChange,
            float statChangePercent,
            List<Category> categories) : this(name, itemTag, description, statToChange, statChangePercent, categories)
        {
            Id = id;
        }
    }
}

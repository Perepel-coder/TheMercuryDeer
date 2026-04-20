using SQLite;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Models
{
    [Table("ItemCategory")]
    public class ItemCategory : BaseModel
    {
        [Column("ItemId")]
        public int ItemId { get; set; }

        [Column("Category")]
        public Category Category { get; set; }
    }
}

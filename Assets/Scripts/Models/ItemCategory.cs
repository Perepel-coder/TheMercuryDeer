using SQLite;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Models
{
    [Table("ItemCategory")]
    public class ItemCategory
    {
        [Column("Id"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("ItemId")]
        public int ItemId { get; set; }

        [Column("Category")]
        public Category Category { get; set; }
    }
}

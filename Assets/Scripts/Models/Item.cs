using SQLite;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Models
{
    [Table("Item")]
    public class Item
    {
        [Column("Id"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("ItemTag")]
        public Tag ItemTag { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Quantity")]
        public int Quantity { get; set; }

        [Column("StatToChange")]
        public StatToChange StatToChange { get; set; }

        [Column("StatChangePercent")]
        public float StatChangePercent { get; set; }
    }
}

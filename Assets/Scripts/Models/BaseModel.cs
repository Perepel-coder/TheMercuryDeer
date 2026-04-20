using SQLite;

namespace Assets.Scripts.Models
{
    public class BaseModel
    {
        [Column("Id"), PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}

using SQLite;

namespace Assets.Scripts.Enums.ItemEnums
{
    public static class ItemDefinitions
    {
        [StoreAsText]
        public enum Category
        {
            Weapon,
            Armor,
            Consumable,
            CraftingMaterial,
        }

        [StoreAsText]
        public enum Tag
        {
            AmorSword,
            PlayerSword,
            BaseReactionToTakingHit,

            RedApple,
        }

        [StoreAsText]
        public enum StatToChange
        {
            Health,
        }
    }
}

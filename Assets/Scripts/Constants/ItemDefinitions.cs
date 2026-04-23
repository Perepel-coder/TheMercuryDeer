using SQLite;

namespace Assets.Scripts.Constants
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
        public enum WeaponTag
        {
            None,
            AmorSword,
            PlayerSword,
            BaseReactionToTakingHit,
        }

        [StoreAsText]
        public enum ItemTag
        {
            None,
            RedApple,
        }

        [StoreAsText]
        public enum StatToChange
        {
            Health,
        }
    }
}

namespace Assets.Scripts.Constants.Paths
{
    public static class ResourcePaths
    {
        private const string PATH_TO_PREFABS = "Prefabs/";

        public static class UI
        {
            private const string PATH_TO_UI_PREFABS = PATH_TO_PREFABS + "UI/";

            public const string HEALTH_SLIDER = PATH_TO_UI_PREFABS + "HealthSlider";
            public const string DAMAGE_POP_UP = PATH_TO_UI_PREFABS + "PopUpDamage";
            public const string HEALTH_POP_UP = PATH_TO_UI_PREFABS + "PopUpHealth";
            public const string HINT_POP_UP = PATH_TO_UI_PREFABS + "PopUpHint";
        }

        public static class Materials
        {
            private const string PATH_TO_MATERIALS = "Materials/";

            public const string FLASH_BLINK_MATERIAL = PATH_TO_MATERIALS + "FlashBlink";
        }

        public static class VFX
        {
            private const string PATH_TO_VFX_PREFABS = PATH_TO_PREFABS + "VFX/";

            public const string DESTRUCTION_EFFECT = PATH_TO_VFX_PREFABS + "BaseDestructionEffectVFX";
        }

        public static class ScriptableObjects
        {
            private const string PATH_TO_SCRIPTABLE_OBJECTS = "ScriptableObjects/";

            public const string PATH_TO_ITEMS = PATH_TO_SCRIPTABLE_OBJECTS + "Items/";
            public const string PATH_TO_ENEMIES = PATH_TO_SCRIPTABLE_OBJECTS + "Enemies/";
            public const string PATH_TO_WEAPONS = PATH_TO_SCRIPTABLE_OBJECTS + "Weapons/";
            public const string PATH_TO_PLAYER_DATA = PATH_TO_SCRIPTABLE_OBJECTS + "Player/";
        }
    }
}

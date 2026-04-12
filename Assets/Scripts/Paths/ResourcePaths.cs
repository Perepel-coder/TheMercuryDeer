namespace Assets.Scripts.Paths
{
    public static class ResourcePaths
    {
        private const string PATH_TO_PREFABS = "Prefabs/";
        private const string PATH_TO_MATERIALS = "Materials/";

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
            public const string FLASH_BLINK_MATERIAL = PATH_TO_MATERIALS + "FlashBlink";
        }

        public static class VFX
        {
            private const string PATH_TO_VFX_PREFABS = PATH_TO_PREFABS + "VFX/";
            public const string DESTRUCTION_EFFECT = PATH_TO_VFX_PREFABS + "BaseDestructionEffectVFX";
        }
    }
}

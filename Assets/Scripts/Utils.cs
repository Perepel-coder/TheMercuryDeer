using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TheMercuryDeer.Scripts.Utils
{
    public static class Utils
    {
        private const string PATH_TO_PREFABS = "Prefabs/";

        public static Vector3 GetRandomDirection()
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            return new Vector3(randomDirection.x, randomDirection.y, 0f);
        }

        public static T GetRandomEnumValue<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(Random.Range(0, values.Length));
        }

        

        public static class UI
        {
            private const string PATH_TO_UI_PREFABS = PATH_TO_PREFABS + "UI/";

            public const string HEALTH_SLIDER = PATH_TO_UI_PREFABS + "HealthSlider";
            public const string DAMAGE_POP_UP = PATH_TO_UI_PREFABS + "PopUpDamage";
            public const string HEALTH_POP_UP = PATH_TO_UI_PREFABS + "PopUpHealth";
        }

        public static class AnimatorParameters
        {

            public const string IS_RUNNING = "isRunning";
            public const string IS_DIE = "isDie";

            public const string ATTACK = "attack";
            public const string TAKE_HIT = "takeHit";

            public const string CHASING_SPEED_MULTIPLIER = "chasingSpeedMultiplier";
        }

        public static class AnimationNames
        {
            public const string TAKE_HIT = "TakeHit";
        }
    }
}

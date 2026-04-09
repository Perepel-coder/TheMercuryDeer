using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public static class Utils
    {
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

        public static float GetPercentage(float value, float percentage) => value * (percentage / 100f);
    }
}

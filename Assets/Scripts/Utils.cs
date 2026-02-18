using UnityEngine;

namespace TheMercuryDeer.Scripts.Utils
{
    public static class Utils
    {
        public static Vector3 GetRandomDirection()
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            return new Vector3(randomDirection.x, randomDirection.y, 0f);
        }
    }
}

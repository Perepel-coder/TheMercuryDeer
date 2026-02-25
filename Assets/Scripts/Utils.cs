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

        public const string IS_RUNNING = "isRunning";
        public const string IS_DIE = "isDie";

        public const string ATTACK = "attack";
        public const string TAKE_HIT = "takeHit";

        public const string CHASING_SPEED_MULTIPLIER = "chasingSpeedMultiplier";
    }
}

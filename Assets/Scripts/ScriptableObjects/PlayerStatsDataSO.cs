using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Stats SO", menuName = "Player Stats SO")]
    public class PlayerStatsDataSO : ScriptableObject
    {
        [SerializeField] private PlayerDefinitions _tag;

        [Header("Health")]
        [SerializeField] private int _maxHealth = 100;

        [Header("Movement")]
        [SerializeField] private float _baseSpeedMoving = 5f;
        [SerializeField] private float _speedMovingMin = 2f;

        [Header("Dash")]
        [SerializeField] private int _dashSpeedMultiplier = 2;
        [SerializeField] private float _dashDuration = 0.5f;
        [SerializeField] private float _dashCooldown = 1f;

        public PlayerDefinitions Tag => _tag;

        public int MaxHealth => _maxHealth;
        public float BaseSpeedMoving => _baseSpeedMoving;
        public float SpeedMovingMin => _speedMovingMin;

        public int DashSpeedMultiplier => _dashSpeedMultiplier;
        public float DashDuration => _dashDuration;
        public float DashCooldown => _dashCooldown;
    }
}

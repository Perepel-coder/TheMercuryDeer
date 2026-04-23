using UnityEngine;
using static Assets.Scripts.Constants.EnemyDefinitions;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Data SO", menuName = "Enemy Data SO")]
    public class EnemyDataSO : ScriptableObject
    {
        [SerializeField] private EnemyTag _tag;
        [SerializeField] private string _enemyName;
        [SerializeField] private Sprite _sprite;

        [Header("Roaming")]
        [SerializeField] private float _roamingDistanceMin = 2f;
        [SerializeField] private float _roamingDistanceMax = 6f;
        [SerializeField] private float _roamingTimeMax = 4f;
        [SerializeField] private float _roamingSpeed = 1f;

        [Header("Chasing")]
        [SerializeField] private float _chasingDistance = 5f;
        [SerializeField] private float _chasingSpeedMultiplier = 2f;

        [Header("Attack")]
        [SerializeField] private float _attackingDistance = 0.5f;
        [SerializeField] private float _attackRate = 2f;
        [SerializeField] private int _inherentDamage = 1;

        [Header("Health")]
        [SerializeField] private int _maxHealth = 30;

        public EnemyTag Tag => _tag;
        public string EnemyName => _enemyName;
        public Sprite Sprite => _sprite;

        public float RoamingDistanceMin => _roamingDistanceMin;
        public float RoamingDistanceMax => _roamingDistanceMax;
        public float RoamingTimeMax => _roamingTimeMax;
        public float RoamingSpeed => _roamingSpeed;

        public float ChasingDistance => _chasingDistance;
        public float ChasingSpeedMultiplier => _chasingSpeedMultiplier;

        public float AttackingDistance => _attackingDistance;
        public float AttackRate => _attackRate;
        public int InherentDamage => _inherentDamage;

        public int MaxHealth => _maxHealth;
    }
}

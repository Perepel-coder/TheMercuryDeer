using UnityEngine;
using static Assets.Scripts.Constants.ItemDefinitions;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon Data SO", menuName = "Weapon Data SO")]
    public class WeaponDataSO : ScriptableObject
    {
        [SerializeField] private WeaponTag _tag;
        [SerializeField] private string _weaponName;
        [SerializeField] private Sprite _sprite;

        [Header("Stats")]
        [SerializeField] private int _healthAmount = 0;
        [SerializeField] private int _damageAmount = 1;
        [SerializeField] private bool _isContinuousDamage = false;
        [SerializeField] private float _dropHeight = 1f;

        [TextArea(3, 5)]
        [SerializeField] private string _description;

        public WeaponTag Tag => _tag;
        public string WeaponName => _weaponName;
        public Sprite Sprite => _sprite;

        public int HealthAmount => _healthAmount;
        public int DamageAmount => _damageAmount;
        public bool IsContinuousDamage => _isContinuousDamage;
        public float DropHeight => _dropHeight;
        public string Description => _description;

        public int RandomDamageAmount => Random.Range(0, DamageAmount + 1);
        public int RandomHealthAmount => Random.Range(0, HealthAmount + 1);
    }
}

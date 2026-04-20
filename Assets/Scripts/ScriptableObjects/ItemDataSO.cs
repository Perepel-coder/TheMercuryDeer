using Assets.Scripts.Services.Player;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Item Data SO", menuName = "Item Data SO")]
    public class ItemDataSO : ScriptableObject
    {
        [SerializeField] private Tag _itemTag;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Vector3 _spriteScale = new Vector3(1, 1, 1);
        [SerializeField] private StatToChange _statToChange;
        [SerializeField] private float _statChangePercent;
        [SerializeField] private List<Category> _categories;
        [SerializeField][TextArea(3, 5)] private string _description;

        public string Name => _name;
        public Tag ItemTag => _itemTag;
        public string Description => _description;
        public StatToChange StatToChange => _statToChange;
        public float StatChangePercent => _statChangePercent;
        public List<Category> Categories => _categories;
        public Sprite Sprite => _sprite;
        public Vector3 SpriteScale => _spriteScale;
        public void UseItem()
        {
            switch (StatToChange)
            {
                case StatToChange.Health:
                    float healthToRestore = Utils.GetPercentage(PlayerService.Instance.MaxHealth, StatChangePercent);
                    PlayerEntityService.Instance.RestoreHealth(healthToRestore);
                    break;
            }
        }
    }
}

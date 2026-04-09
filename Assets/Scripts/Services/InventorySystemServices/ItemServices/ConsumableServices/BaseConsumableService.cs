using Assets.Scripts.DTO;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.Player;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices.ItemServices.ConsumableServices
{
    public class BaseConsumableService : InventoryItemService
    {
        [SerializeField] protected Tag _itemTag;

        public override ItemDTO ItemData { get; protected set; }

        protected override void Awake()
        {
            base.Awake();
            ItemData = DatabaseService.ItemRepository.GetItemByTag(_itemTag);
        }

        protected override void Start()
        {
            base.Start();
        }

        protected override void UseItem()
        {
            switch (ItemData.StatToChange)
            {
                case StatToChange.Health:
                    float healthToRestore = Utils.GetPercentage(PlayerService.Instance.MaxHealth, ItemData.StatChangePercent);
                    PlayerEntityService.Instance.RestoreHealth(healthToRestore);
                    break;
            }
        }
    }
}
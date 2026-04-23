using Assets.InputActions;
using Assets.Scripts.Constants.Paths;
using Assets.Scripts.Interfaces.Inventory;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Services.InventorySystemServices.UI;
using Assets.Scripts.Services.Player;
using Assets.Scripts.Views;
using System;
using UnityEngine;
using static Assets.Scripts.Constants.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices
{
    public class ItemService : MonoBehaviour, IInventoryItem
    {
        [SerializeField] private ItemTag _itemTag = ItemTag.None;

        protected InventoryManagerService _inventoryManagerService;
        private float _radiusInteractionZone = 1f;

        public bool IsPlayerInRange { get; private set; }

        protected virtual void Awake()
        {
            _inventoryManagerService = GameObject.Find(GameObjectNames.INVENTORY_CANVAS).GetComponent<InventoryManagerService>();
        }

        protected virtual void Start()
        {
            GameInput.Instance.OnPlayerInteractWithItem += GameInput_OnPlayerTookItem;
        }

        protected virtual void OnDestroy()
        {
            GameInput.Instance.OnPlayerInteractWithItem -= GameInput_OnPlayerTookItem;
        }

        protected virtual void Update()
        {
            IsPlayerInRange = Vector2.Distance(transform.position, PlayerService.Instance.transform.position) <= _radiusInteractionZone;
        }

        protected virtual void GameInput_OnPlayerTookItem(object sender, EventArgs e)
        {
            if (!IsPlayerInRange) return;

            if (_inventoryManagerService.AddItemToInventory(_itemTag))
                Destroy(gameObject);
        }

        public void SetItemTag(ItemTag tag) => _itemTag = tag;

        public static void CreateItem(ItemDataSO itemData)
        {
            GameObject gameObject = new(itemData.Tag.ToString());

            gameObject.AddComponent<ItemService>().SetItemTag(itemData.Tag);
            gameObject.AddComponent<ItemServiceView>();
            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.AddComponent<SpriteRenderer>().sprite = itemData.Sprite;
            gameObject.transform.localScale = itemData.SpriteScale;
            gameObject.transform.position = PlayerService.Instance.transform.position + Utils.GetRandomDirection(2f);
        }
    }
}

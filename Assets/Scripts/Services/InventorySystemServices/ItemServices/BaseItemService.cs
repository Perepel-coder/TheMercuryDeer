using Assets.InputActions;
using Assets.Scripts.Application.Interfaces.Inventory;
using Assets.Scripts.DTO;
using Assets.Scripts.Paths;
using Assets.Scripts.Services.InventorySystemServices.UI;
using Assets.Scripts.Services.Player;
using Assets.Scripts.Services.UIServices;
using SuperTiled2Unity.Editor;
using System;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices.ItemServices
{
    public abstract class BaseItemService : MonoBehaviour, IInventoryItem
    {
        [SerializeField] protected Tag _itemTag = Tag.None;

        public abstract ItemDTO ItemData { get; protected set; }
        protected abstract void UseItem();

        protected InventoryManagerService _inventoryManagerService;
        private PopUpHintService _popUpHintService;
        private float _radiusInteractionZone = 1f;
        private bool _isPlayerInRange = false;

        protected virtual void Awake()
        {
            _inventoryManagerService = GameObject.Find(GameObjectNames.INVENTORY_CANVAS).GetComponent<InventoryManagerService>();
        }

        protected virtual void Start()
        {
            ItemData.UseItem = UseItem;
            ItemData.ItemType = GetType();
            ItemData.Scale = transform.localScale;
            ItemData.Sprite = GetComponentInChildren<SpriteRenderer>().sprite;

            _popUpHintService = Instantiate(
                Resources.Load<PopUpHintService>(ResourcePaths.UI.HINT_POP_UP),
                transform.position + new Vector3(0, _radiusInteractionZone, 0),
                Quaternion.identity);

            _popUpHintService.EraseText();

            GameInput.Instance.OnPlayerInteractWithItem += GameInput_OnPlayerTookItem;
        }

        protected virtual void OnDestroy()
        {
            GameInput.Instance.OnPlayerInteractWithItem -= GameInput_OnPlayerTookItem;

            if (_popUpHintService != null)
                Destroy(_popUpHintService.gameObject);
        }

        protected virtual void Update()
        {
            CheckHintPopUp();
        }

        private void CheckHintPopUp()
        {
            bool inRange = Vector2.Distance(transform.position, PlayerService.Instance.transform.position) <= _radiusInteractionZone;

            if (inRange && !_isPlayerInRange)
                _popUpHintService.Draw("е");

            else if (!inRange && _isPlayerInRange)
                _popUpHintService.EraseText();

            _isPlayerInRange = inRange;
        }

        protected virtual void GameInput_OnPlayerTookItem(object sender, EventArgs e)
        {
            if (!_isPlayerInRange) return;

            if (_inventoryManagerService.AddItemToInventory(ItemData))
                Destroy(gameObject);
        }

        public void SetItemTag(Tag tag) => _itemTag = tag;

        public static void CreateItem(ItemDTO itemData)
        {
            GameObject gameObject = new(itemData.ItemTag.ToString());
            GameObject gameObjectView = new($"{itemData.ItemTag.ToString()}View");
            gameObject.AddChildWithUniqueName(gameObjectView);

            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            (gameObject.AddComponent(itemData.ItemType) as BaseItemService).SetItemTag(itemData.ItemTag);
            gameObjectView.AddComponent<SpriteRenderer>().sprite = itemData.Sprite;

            gameObject.transform.localScale = itemData.Scale;
            gameObject.transform.position = PlayerService.Instance.transform.position + Utils.GetRandomDirection(2f);
        }
    }
}

using Assets.InputActions;
using Assets.Scripts.Application.Interfaces.Inventory;
using Assets.Scripts.Constans.Paths;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Services.InventorySystemServices.UI;
using Assets.Scripts.Services.Player;
using Assets.Scripts.Services.UIServices;
using System;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices.ItemServices
{
    public class ItemService : MonoBehaviour, IInventoryItem
    {
        [SerializeField] protected Tag _itemTag = Tag.None;

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

            if (_inventoryManagerService.AddItemToInventory(_itemTag))
                Destroy(gameObject);
        }

        public void SetItemTag(Tag tag) => _itemTag = tag;

        public static void CreateItem(ItemDataSO itemData)
        {
            GameObject gameObject = new(itemData.ItemTag.ToString());

            gameObject.AddComponent<ItemService>().SetItemTag(itemData.ItemTag);
            gameObject.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            gameObject.AddComponent<SpriteRenderer>().sprite = itemData.Sprite;
            gameObject.transform.localScale = itemData.SpriteScale;
            gameObject.transform.position = PlayerService.Instance.transform.position + Utils.GetRandomDirection(2f);
        }
    }
}

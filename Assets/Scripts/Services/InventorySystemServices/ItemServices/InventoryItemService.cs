using Assets.InputActions;
using Assets.Scripts.Application.Interfaces.Inventory;
using Assets.Scripts.DTO;
using Assets.Scripts.Paths;
using Assets.Scripts.Services.InventorySystemServices.UI;
using Assets.Scripts.Services.Player;
using Assets.Scripts.Services.UIServices;
using System;
using UnityEngine;

namespace Assets.Scripts.Services.InventorySystemServices.ItemServices
{
    public abstract class InventoryItemService : MonoBehaviour, IInventoryItem
    {
        public abstract ItemDTO ItemData { get; protected set; }

        protected InventoryManagerService _inventoryManagerService;
        private PopUpHintService _popUpHintService;
        private float _radiusInteractionZone = 1f;
        private bool _isPlayerInRange = false;

        protected virtual void Awake()
        {
            _popUpHintService = Instantiate(
                Resources.Load<PopUpHintService>(ResourcePaths.UI.HINT_POP_UP), 
                transform.position + new Vector3(0, _radiusInteractionZone, 0), 
                Quaternion.identity);

            _popUpHintService.EraseText();

            _inventoryManagerService = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagerService>();
        }

        protected virtual void Start()
        {
            ItemData.UseItem = UseItem;
            ItemData.Sprite = GetComponentInChildren<SpriteRenderer>().sprite;
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
            bool inRange = Vector2.Distance(transform.position, PlayerService.Instance.transform.position) <=
                _radiusInteractionZone;

            if (inRange && !_isPlayerInRange)
                _popUpHintService.Draw("E", Vector2.up);

            else if (!inRange && _isPlayerInRange)
                _popUpHintService.EraseText();

            _isPlayerInRange = inRange;
        }

        protected virtual void GameInput_OnPlayerTookItem(object sender, EventArgs e)
        {
            if (!_isPlayerInRange) return;

            if(_inventoryManagerService.AddItemToInventory(ItemData))
                Destroy(gameObject);
        }

        protected virtual void UseItem() => Debug.Log($"Невозможно использовать предмет");
    }
}

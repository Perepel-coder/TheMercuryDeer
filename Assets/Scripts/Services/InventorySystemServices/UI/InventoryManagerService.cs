using Assets.InputActions;
using Assets.Scripts.DTO;
using System;
using UnityEngine;

namespace Assets.Scripts.Services.InventorySystemServices.UI
{
    public class InventoryManagerService : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryMenu;
        private InventorySlotsPanel _inventorySlotsPanel;

        private void Awake()
        {
            _inventoryMenu.SetActive(true);
            _inventorySlotsPanel = _inventoryMenu.GetComponentInChildren<InventorySlotsPanel>();
        }

        private void Start()
        {
            var descriptionPanel = _inventoryMenu.GetComponentInChildren<InventoryDescriptionPanel>();
            foreach (var slot in _inventorySlotsPanel.InventorySlots)
                slot.Initialize(descriptionPanel);

            _inventoryMenu.SetActive(false);
            GameInput.Instance.OnPlayerOpenInventory += GameInput_OnPlayerOpenInventory;
        }

        private void OnDestroy()
        {
            GameInput.Instance.OnPlayerOpenInventory -= GameInput_OnPlayerOpenInventory;
        }

        private void GameInput_OnPlayerOpenInventory(object sender, EventArgs e)
        {
            _inventoryMenu.SetActive(!_inventoryMenu.activeSelf);

            Time.timeScale = _inventoryMenu.activeSelf ? 0f : 1f;
            GameInput.Instance.SetCombatEnabled(!_inventoryMenu.activeSelf);
        }

        public bool AddItemToInventory(ItemDTO item)
        {
            InventorySlot slot = _inventorySlotsPanel.GetSlotByItemTag(item.ItemTag) ?? _inventorySlotsPanel.GetEmptySlot();

            if (slot != null)
            {
                slot.AddItemToSlot(item);
                return true;
            }

            return false;   
        }
    }
}
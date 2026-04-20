using Assets.InputActions;
using Assets.Scripts.Constans.Paths;
using Assets.Scripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices.UI
{
    public class InventoryManagerService : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryMenu;
        private List<InventorySlot> _inventorySlots;

        private void Awake()
        {
            _inventoryMenu.SetActive(true);
            _inventorySlots = GetComponentsInChildren<InventorySlot>().ToList();
        }

        private void Start()
        {
            InitialInventorySlots(_inventoryMenu.GetComponentInChildren<InventoryDescriptionPanel>());

            _inventoryMenu.SetActive(false);
            GameInput.Instance.OnPlayerOpenInventory += GameInput_OnPlayerOpenInventory;
        }

        private void OnDestroy()
        {
            GameInput.Instance.OnPlayerOpenInventory -= GameInput_OnPlayerOpenInventory;
            foreach(var slot in _inventorySlots)
            {
                slot.OnUseItem -= _inventorySlot_OnUseItem;
            }
        }

        private void InitialInventorySlots(InventoryDescriptionPanel descriptionPanel)
        {
            for(int index = 0; index < _inventorySlots.Count; index++)
            {
                var slot= _inventorySlots[index];
                slot.Initialize(index, descriptionPanel);
                slot.OnUseItem += _inventorySlot_OnUseItem;
            }
        }

        private InventorySlot GetEmptySlot() => _inventorySlots?.FirstOrDefault(slot => slot.IsEmpty);

        public InventorySlot GetSlotByItemTag(Tag itemTag) => _inventorySlots?.FirstOrDefault(slot => !slot.IsEmpty && slot.ItemTag == itemTag);

        private void GameInput_OnPlayerOpenInventory(object sender, EventArgs e)
        {
            _inventoryMenu.SetActive(!_inventoryMenu.activeSelf);

            Time.timeScale = _inventoryMenu.activeSelf ? 0f : 1f;

            GameInput.Instance.SetCombatEnabled(!_inventoryMenu.activeSelf);
        }

        /// <summary>
        /// Использовать предмет из инвентаря при клике на него. Вызывает действие, связанное с предметом, если оно есть.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _inventorySlot_OnUseItem(object sender, EventArgs e)
        {
            var slot = sender as InventorySlot;
            slot?.InventoryItemData?.UseItem();
        }

        /// <summary>
        /// Добавляет предмет в инвентарь. Если предмет уже есть, то он будет добавлен в существующий слот, 
        /// иначе будет добавлен в первый свободный слот. Если свободных слотов нет, то предмет не будет добавлен.
        /// </summary>
        /// <param name="itemData"></param>
        /// <returns></returns>
        public bool AddItemToInventory(Tag itemTag)
        {
            InventorySlot slot = GetSlotByItemTag(itemTag) ?? GetEmptySlot();
            ItemDataSO item = Resources.Load<ItemDataSO>($"{ResourcePaths.ScriptableObjects.PATH_TO_ITEMS}{itemTag}");

            if (slot != null && item != null)
            {
                slot.AddItemToSlot(item);
                return true;
            }

            return false;
        }
    }
}
using Assets.Scripts.Constans.Paths;
using Assets.Scripts.ScriptableObjects;
using Assets.Scripts.Services.InventorySystemServices.ItemServices;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices.UI
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        private const int MAX_QUANTITY_IN_SLOT = 10;

        private Sprite _defaultIcon;
        private Image _itemImage;
        private TMP_Text _quantityText;
        private InventoryDescriptionPanel _inventoryDescriptionPanel;

        private int _quantity;
        public ItemDataSO InventoryItemData { get; private set; }

        public event EventHandler OnUseItem;

        public int InventorySlotIndex { get; private set; }
        public Tag ItemTag => InventoryItemData?.ItemTag ?? Tag.None;
        public bool IsEmpty => InventoryItemData == null;
        public bool IsFull => _quantity >= MAX_QUANTITY_IN_SLOT;


        private void Awake()
        {
            _itemImage = transform.Find(GameObjectNames.ICON).GetComponent<Image>();
            _quantityText = transform.Find(GameObjectNames.QUANTITY).GetComponent<TMP_Text>();
            _defaultIcon = _itemImage.sprite;
        }

        private void OnDestroy()
        {
            if (_inventoryDescriptionPanel != null)
                _inventoryDescriptionPanel.OnUseItem -= _inventoryDescriptionPanel_OnUseItem;
        }

        public void Initialize(int inventorySlotIndex, InventoryDescriptionPanel descriptionPanel)
        {
            InventorySlotIndex = inventorySlotIndex;
            _inventoryDescriptionPanel = descriptionPanel;
            _inventoryDescriptionPanel.OnUseItem += _inventoryDescriptionPanel_OnUseItem;
        }

        private void _inventoryDescriptionPanel_OnUseItem(object sender, EventArgs e)
        {
            OnUseItem?.Invoke(this, EventArgs.Empty);
            DelItemFromSlot();
        }

        private void DelItemFromSlot(int quantity = 1, bool needDrawSlot = true)
        {
            if (InventoryItemData == null) return;

            _quantity -= quantity;

            if (_quantity <= 0) ClearSlot();
            else if (needDrawSlot) DrawSlot();
        }

        private void ClearSlot()
        {
            InventoryItemData = null;
            _quantity = 0;
            _itemImage.sprite = _defaultIcon;
            _quantityText.text = string.Empty;

            _inventoryDescriptionPanel.ClearDescription();
        }

        private void DrawSlot()
        {
            _itemImage.sprite = InventoryItemData.Sprite;
            _quantityText.text = _quantity.ToString();
        }

        public void AddItemToSlot(ItemDataSO itemData, bool needDrawSlot = true)
        {
            if (InventoryItemData == null) InventoryItemData = itemData;
            _quantity++;

            if (needDrawSlot) DrawSlot();
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
                _inventoryDescriptionPanel.DrawDescription(
                    InventoryItemData.Sprite,
                    InventoryItemData.Name,
                    InventoryItemData.Description);

            if (pointerEventData.button == PointerEventData.InputButton.Right && InventoryItemData != null)
            {
                for (int i = 0; i < _quantity; i++)
                    ItemService.CreateItem(InventoryItemData);

                ClearSlot();
            }
        }
    }
}

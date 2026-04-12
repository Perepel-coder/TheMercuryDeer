using Assets.Scripts.DTO;
using Assets.Scripts.Services.InventorySystemServices.ItemServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Services.InventorySystemServices.UI
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        private const int MAX_QUANTITY_IN_SLOT = 10;
        private Sprite _defaultIcon;

        private InventoryDescriptionPanel _inventoryDescriptionPanel;

        private Image _itemImage;
        private TMP_Text _quantityText;

        public ItemDTO ItemData { get; private set; }

        public bool IsEmpty => ItemData == null;
        public bool IsFull => ItemData?.Quantity >= MAX_QUANTITY_IN_SLOT;
        public bool IsSelected { get; set; }

        private void Awake()
        {
            _itemImage = transform.Find("Icon").GetComponent<Image>();
            _quantityText = transform.Find("Quantity").GetComponent<TMP_Text>();
            _defaultIcon = _itemImage.sprite;
        }

        private void OnDestroy()
        {
            if (_inventoryDescriptionPanel != null)
                _inventoryDescriptionPanel.OnUseItem -= _inventoryDescriptionPanel_OnUseItem;
        }

        public void Initialize(InventoryDescriptionPanel descriptionPanel)
        {
            _inventoryDescriptionPanel = descriptionPanel;
            _inventoryDescriptionPanel.OnUseItem += _inventoryDescriptionPanel_OnUseItem;
        }

        private void _inventoryDescriptionPanel_OnUseItem(object sender, System.EventArgs e) => DelItemFromSlot();

        public void AddItemToSlot(ItemDTO item, bool needDrawSlot = true)
        {
            if (ItemData == null) ItemData = item;
            else ItemData.Quantity += item.Quantity;

            if (needDrawSlot) DrawSlot();
        }

        private void DelItemFromSlot(int quantity = 1, bool needDrawSlot = true)
        {
            if (ItemData == null) return;

            ItemData.Quantity -= quantity;

            if (ItemData.Quantity <= 0) ClearSlot();

            else if (needDrawSlot) DrawSlot();
        }

        private void ClearSlot()
        {
            ItemData = null;
            _itemImage.sprite = _defaultIcon;
            _quantityText.text = string.Empty;

            _inventoryDescriptionPanel.ClearDescription();
        }

        private void DrawSlot()
        {
            _itemImage.sprite = ItemData.Sprite;
            _quantityText.text = ItemData.Quantity.ToString();
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
                _inventoryDescriptionPanel.DrawDescription(ItemData);

            if (pointerEventData.button == PointerEventData.InputButton.Right && ItemData != null)
            {
                for (int i = 0; i < ItemData.Quantity; i++)
                    BaseItemService.CreateItem(ItemData);

                ClearSlot();
            }
        }
    }
}

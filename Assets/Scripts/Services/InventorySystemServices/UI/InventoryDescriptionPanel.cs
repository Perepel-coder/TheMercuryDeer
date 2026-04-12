using Assets.Scripts.DTO;
using Assets.Scripts.Paths;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Services.InventorySystemServices.UI
{
    public class InventoryDescriptionPanel : MonoBehaviour
    {
        private ItemDTO _itemData;

        private Sprite _defaultIcon;

        private Image _itemImage;
        private TMP_Text _ItemName;
        private TMP_Text _ItemDescription;
        private Button _useItemButton;

        public event EventHandler OnUseItem;

        private void Awake()
        {
            _itemImage = transform.Find(GameObjectNames.ITEM_IMAGE).Find(GameObjectNames.ICON).GetComponent<Image>();
            _ItemName = transform.Find(GameObjectNames.ITEM_DESCRIPTION).Find(GameObjectNames.ITEM_NAME).GetComponent<TMP_Text>();
            _ItemDescription = transform.Find(GameObjectNames.ITEM_DESCRIPTION).Find(GameObjectNames.ITEM_DESCRIPTION).GetComponent<TMP_Text>();
            _useItemButton = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _defaultIcon = _itemImage.sprite;
            _useItemButton.onClick.AddListener(OnUseItemButtonClicked);
        }

        private void OnDestroy()
        {
            _useItemButton.onClick.RemoveListener(OnUseItemButtonClicked);
        }

        private void OnUseItemButtonClicked()
        {
            if (_itemData == null) return;
            _itemData?.UseItem?.Invoke();

            OnUseItem?.Invoke(this, EventArgs.Empty);
        }

        public void DrawDescription(ItemDTO item)
        {
            ClearDescription();

            if (item == null) return;

            _itemData = item;
            _itemImage.sprite = item.Sprite;
            _ItemName.text = item.Name.ToLower();
            _ItemDescription.text = item.Description.ToLower();
        }

        public void ClearDescription()
        {
            _itemData = null;
            _itemImage.sprite = _defaultIcon;
            _ItemName.text = string.Empty;
            _ItemDescription.text = string.Empty;
        }
    }
}

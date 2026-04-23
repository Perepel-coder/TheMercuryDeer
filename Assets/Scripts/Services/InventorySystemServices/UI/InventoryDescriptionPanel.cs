using Assets.Scripts.Constants.Paths;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Services.InventorySystemServices.UI
{
    public class InventoryDescriptionPanel : MonoBehaviour
    {
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

        private void OnUseItemButtonClicked() => OnUseItem?.Invoke(this, EventArgs.Empty);

        public void DrawDescription(Sprite sprite, string itemName, string itemDesription)
        {
            ClearDescription();

            _itemImage.sprite = sprite;
            _ItemName.text = itemName.ToLower();
            _ItemDescription.text = itemDesription.ToLower();
        }

        public void ClearDescription()
        {
            _itemImage.sprite = _defaultIcon;
            _ItemName.text = string.Empty;
            _ItemDescription.text = string.Empty;
        }
    }
}

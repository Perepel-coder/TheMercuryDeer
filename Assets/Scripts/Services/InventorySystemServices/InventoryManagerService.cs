using Assets.InputActions;
using System;
using UnityEngine;

namespace Assets.Scripts.Services.InventorySystem
{
    public class InventoryManagerService : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryMenu;

        private void Start()
        {
            _inventoryMenu.SetActive(false);
            GameInput.Instance.OnPlayerOpenInventory += GameInput_OnPlayerOpenInventory;
        }

        private void GameInput_OnPlayerOpenInventory(object sender, EventArgs e)
        {
            _inventoryMenu.SetActive(!_inventoryMenu.activeSelf);

            Time.timeScale = _inventoryMenu.activeSelf ? 0f : 1f;
        }

        private void OnDestroy()
        {
            GameInput.Instance.OnPlayerOpenInventory -= GameInput_OnPlayerOpenInventory;
        }
    }
}
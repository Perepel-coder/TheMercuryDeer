using System.Linq;
using UnityEngine;
using static Assets.Scripts.Enums.ItemEnums.ItemDefinitions;

namespace Assets.Scripts.Services.InventorySystemServices.UI
{
    public class InventorySlotsPanel : MonoBehaviour
    {
        public InventorySlot[] InventorySlots { get; private set; }

        private void Awake()
        {
            InventorySlots = GetComponentsInChildren<InventorySlot>();
        }

        public InventorySlot GetEmptySlot() =>
            InventorySlots.FirstOrDefault(slot => slot.IsEmpty);

        public InventorySlot GetSlotByItemTag(Tag itemTag) =>
            InventorySlots.FirstOrDefault(slot => !slot.IsEmpty && slot.ItemData.ItemTag == itemTag);
    }
}

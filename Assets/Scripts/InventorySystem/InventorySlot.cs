using System;
using ItemSystem;

namespace InventorySystem
{
    [Serializable]
    public class InventorySlot
    {
        public InventorySlot(int slotId, bool isEquipmentSlot)
        {
            SlotId = slotId;
            IsEquipmentSlot = isEquipmentSlot;
            Item = null; // Инициализируем ItemInfo значением по умолчанию (null)
        }

        public Item Item { get; set; }
        public int SlotId { get; }
        public bool IsEmpty { get; set; } = true;
        public bool IsEquipmentSlot { get; set; }
    }
}
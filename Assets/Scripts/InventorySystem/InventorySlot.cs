using System;
using ItemSystem;

    [Serializable]
    public class InventorySlot
    {
        public InventorySlot(int slotId, bool isEquipmentSlot)
        {
            SlotId = slotId;
            IsEquipmentSlot = isEquipmentSlot;
            ItemBase = null;
        }

        public ItemBase ItemBase { get; set; }
        public int SlotId { get; }
        public bool IsEmpty { get; set; } = true;
        public bool IsEquipmentSlot { get; set; }
    }
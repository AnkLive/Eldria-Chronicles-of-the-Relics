using System;

[Serializable]
public class InventorySlot
{
    public InventorySlot(int slotId, string itemId, bool isEmpty, bool isEquipment)
    {
        ItemId = itemId;
        SlotId = slotId;
        IsEmpty = isEmpty;
        IsEquipment = isEquipment;
    }

    public string ItemId { get; set; }
    public int SlotId { get; }
    public bool IsEquipment { get; }
    public bool IsEmpty { get; set; }
}
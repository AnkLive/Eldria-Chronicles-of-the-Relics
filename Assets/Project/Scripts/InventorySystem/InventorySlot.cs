using System;
using UnityEngine;

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

    [field: SerializeField] public string ItemId { get; set; }
    [field: SerializeField] public int SlotId { get; set; }
    [field: SerializeField] public bool IsEquipment { get; set; }
    [field: SerializeField] public bool IsEmpty { get; set; }
}
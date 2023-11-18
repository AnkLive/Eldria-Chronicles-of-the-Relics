using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class Inventory
{
    [JsonIgnore] public ItemStorage ItemStorage { get; set; }
    public Dictionary<EItemType, InventorySection> Sections { get; }
    
    public Inventory(Dictionary<EItemType, InventorySection> sections)
    {
        Sections = sections;
    }
    
    public void ExchangeSlotData(int slotId, EItemType inventorySection)
    {
        string itemId = Sections[inventorySection].GetItemIdBySlotId(slotId);
        Sections[inventorySection].MoveItem(ItemStorage.GetItemById(itemId), slotId);
    }
}
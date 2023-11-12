using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    public Dictionary<EItemType, InventorySection> Sections { get; }
    
    public Inventory(Dictionary<EItemType, InventorySection> sections)
    {
        Sections = sections;
    }
}
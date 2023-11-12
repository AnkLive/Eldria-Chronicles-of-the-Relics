using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Storage", menuName = "Inventory/Item Storage", order = 0)]
public class ItemStorage : ScriptableObject
{
    [field: SerializeField] public List<ItemBase> Storage { get; set; } = new();
    
    public ItemBase GetItemById(string id)
    {
        foreach (var item in Storage)
        {
            if (item.ItemId == id)
            {
                return item;
            }
        }
        return null;
    }
}
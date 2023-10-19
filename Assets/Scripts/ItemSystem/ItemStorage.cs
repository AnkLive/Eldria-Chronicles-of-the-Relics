using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(fileName = "New Item Storage", menuName = "Inventory/Item Storage", order = 0)]
    public class ItemStorage : ScriptableObject
    {
        [field: SerializeField] public List<ItemDescription> ItemDescriptions { get; set; } = new();
        
        public ItemDescription GetItemDescriptionById(string id)
        {
            foreach (var item in ItemDescriptions)
            {
                if (item.itemId == id)
                {
                    return item;
                }
            }
            return null;
        }
    }

    [Serializable]
    public class ItemDescription
    {
        public string itemId;
        public Sprite sprite;
        public string description;
    }
}
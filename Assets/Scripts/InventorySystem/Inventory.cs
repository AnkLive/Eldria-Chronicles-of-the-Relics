using System;
using System.Collections.Generic;
using ItemSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class Inventory
    {
        public Inventory(ItemStorage itemStorage)
        {
            _itemStorage = itemStorage;
        }
        
        [JsonIgnore] private ItemStorage _itemStorage;

        public Dictionary<EItemType, InventorySection> Sections { get; set; } = new();
        
        public void AddItem(Item item)
        {
            for (int i = 0; i < Sections.Count; i++)
            {
                EItemType type = (EItemType)i;
                Debug.Log($"Поиск соответствия предмета - {item.ItemId} с инвентарем - {type}");
                
                if (item.ItemType == Sections[type].InventoryType)
                {
                    Debug.Log($"Попытка добавить предмет - {item.ItemId} в инвентарь - {type}");
                    item.Icon = _itemStorage.GetItemDescriptionById(item.ItemId).sprite;
                    Sections[type].AddItem(item);
                    return;
                }
            }
            Debug.Log("Такого типа предмета не существует");
        }
        
        public void InitializeInventorySections(Transform inventoryPanel, Transform equipmentPanel, float startTotalStrengthLimitArtefactSection, float startTotalStrengthLimitSpellSection)
        {
            foreach (var itemType in Enum.GetValues(typeof(EItemType)))
            {
                switch (itemType)
                {
                    case EItemType.Artefact:
                        Sections.Add((EItemType)itemType, new InventorySection((EItemType)itemType, startTotalStrengthLimitArtefactSection));
                        break;
                    case EItemType.Spell:
                        Sections.Add((EItemType)itemType, new InventorySection((EItemType)itemType, startTotalStrengthLimitSpellSection));
                        break;
                    case EItemType.Weapon:
                        Sections.Add((EItemType)itemType, new InventorySection((EItemType)itemType, 0));
                        break;
                }
            }

            foreach (var section in Sections.Values)
            {
                section.Initialize(inventoryPanel.GetChild((int)section.InventoryType), false);
            }

            foreach (var section in Sections.Values)
            {
                section.Initialize(equipmentPanel.GetChild((int)section.InventoryType), true);
            }
        }
        
        public void LoadInventoryItems(InventorySaveLoader inventorySaveLoader)
        {
            foreach (var itemType in Enum.GetValues(typeof(EItemType)))
            {
                var type = (EItemType)itemType;
                
                if (inventorySaveLoader.GetData(type) != null)
                {
                    Sections[type].TotalStrengthLimit = inventorySaveLoader.GetData(type).TotalStrengthLimit;
                    Sections[type].CurrentTotalStrength = inventorySaveLoader.GetData(type).CurrentTotalStrength;
                    int count = inventorySaveLoader.GetData(type).InventorySection.Count;
                    
                    for (int i = 0; i < count; i++)
                    {
                        var item = inventorySaveLoader.GetData(type).InventorySection[i].Item;
                        item.Icon = _itemStorage.GetItemDescriptionById(item.ItemId).sprite;
                        item.ItemDescription = _itemStorage.GetItemDescriptionById(item.ItemId).description;
                        Sections[type].SetSlotById(item, inventorySaveLoader.GetData(type).InventorySection[i].SlotId);
                    }
                }
            }
        }
    }
}
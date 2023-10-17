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
        [JsonIgnore] public ItemStorage itemStorage;

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
                    item.Icon = itemStorage.GetItemDescriptionById(item.ItemId).sprite;
                    Sections[type].AddItem(item);
                    return;
                }
            }
            Debug.Log("Такого типа предмета не существует");
        }
        
        public void InitializeInventorySections(Inventory inventory, Transform inventoryPanel, Transform equipmentPanel, float startTotalStrengthLimitArtefactSection, float startTotalStrengthLimitSpellSection)
        {
            foreach (EItemType type in Enum.GetValues(typeof(EItemType)))
            {
                InventorySection section = new InventorySection
                    (type, type == EItemType.Artefact ? 
                        startTotalStrengthLimitArtefactSection : 
                        type == EItemType.Spell ? 
                            startTotalStrengthLimitSpellSection : 
                            0
                    );

                if (inventory?.Sections[type] != null)
                {
                    section.InventorySlotList = inventory.Sections[type].InventorySlotList;
                    section.TotalStrengthLimit = inventory.Sections[type].TotalStrengthLimit;
                    section.CurrentTotalStrength = inventory.Sections[type].CurrentTotalStrength;
                }
                else
                {
                    section.AddSlot(inventoryPanel.GetChild((int)type), false);
                    section.AddSlot(equipmentPanel.GetChild((int)type), true);
                }

                section.AddIconSetter(inventoryPanel.GetChild((int)type));
                section.AddIconSetter(equipmentPanel.GetChild((int)type));

                Sections.Add(type, section);
            }
        }

    }
}
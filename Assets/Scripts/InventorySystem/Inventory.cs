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
        
        public void AddItem(ItemBase itemBase)
        {
            for (int i = 0; i < Sections.Count; i++)
            {
                EItemType type = (EItemType)i;
                Debug.Log($"Поиск соответствия предмета - {itemBase.ItemId} с инвентарем - {type}");
                
                if (itemBase.ItemType == Sections[type].InventoryType)
                {
                    Debug.Log($"Попытка добавить предмет - {itemBase.ItemId} в инвентарь - {type}");
                    itemBase.Icon = itemStorage.GetItemDescriptionById(itemBase.ItemId).sprite;
                    Sections[type].AddItem(itemBase);
                    return;
                }
            }
            Debug.Log("Такого типа предмета не существует");
        }
        
        public void InitializeInventorySections(Inventory inventory, Transform inventoryPanel, Transform equipmentPanel, float startTotalStrengthLimitArtefactSection, float startTotalStrengthLimitSpellSection)
        {
            foreach (EItemType type in Enum.GetValues(typeof(EItemType)))
            {
                Sections.Add(type, new InventorySection(type, type == EItemType.Weapon ? 0 : (type == EItemType.Artefact ? startTotalStrengthLimitArtefactSection : startTotalStrengthLimitSpellSection)));
        
                if (inventory?.Sections.ContainsKey(type) == true)
                {
                    Sections[type].InventorySlotList = inventory.Sections[type].InventorySlotList;
                    Sections[type].TotalStrengthLimit = inventory.Sections[type].TotalStrengthLimit;
                    Sections[type].CurrentTotalStrength = inventory.Sections[type].CurrentTotalStrength;
                    Sections[type].AddIconSetter(inventoryPanel.GetChild((int)type));
                    Sections[type].AddIconSetter(equipmentPanel.GetChild((int)type));
            
                    foreach (var slot in inventory.Sections[type].InventorySlotList)
                    {
                        if (slot?.ItemBase != null)
                        {
                            slot.ItemBase.Icon = itemStorage.GetItemDescriptionById(slot.ItemBase.ItemId).sprite;
                            Sections[type].IconSetterList[slot.SlotId].SetIcon(slot.ItemBase.Icon);
                        }
                    }
                }
                else
                {
                    Sections[type].AddSlot(inventoryPanel.GetChild((int)type), false);
                    Sections[type].AddSlot(equipmentPanel.GetChild((int)type), true);
                }
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using ItemSystem;
using Newtonsoft.Json;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace InventorySystem
{
    [Serializable]
    public class InventorySection
    {
        public InventorySection(EItemType inventoryType, float totalStrengthLimit)
        {
            InventoryType = inventoryType;
            TotalStrengthLimit = totalStrengthLimit;
        }
        
        public List<InventorySlot> InventorySlotList { get; set; } = new();
        
        [JsonIgnore] public List<UIItemIconSetter> IconSetterList { get; set; } = new();
        
        [field: JsonIgnore] public event Action<List<ItemBase>> OnEquippedItem = delegate {  };
        [field: JsonIgnore] public event Action OnChangedStrengthInventory = delegate {  };
        [field: JsonIgnore] public event Action OnAddItem = delegate {  };
        
        public float TotalStrengthLimit { get; set; }
        public float CurrentTotalStrength { get; set; }

        public EItemType InventoryType { get; }

        public int GetCountSlotInventory()
        {
            return InventorySlotList.Count;
        }

        public List<ItemBase> GetAllEquipmentItems()
        {
            List<ItemBase> list = new();
            
            foreach (var slot in InventorySlotList)
            {
                if (slot.ItemBase != null && slot.ItemBase.IsEquipment)
                {
                    list.Add(slot.ItemBase);
                    Debug.Log(slot.ItemBase.IsEquipment);
                }
            }
            return list;
        }
        
        public void SetSlotById(ItemBase itemBase, int slotId)
        {
            var slot = InventorySlotList.Find(s => s.SlotId == slotId);

            if (slot != null)
            { 
                slot.ItemBase = itemBase;
                slot.IsEmpty = false;
                var iconSetter = IconSetterList[slotId];
                iconSetter.SetIcon(itemBase.Icon);
                iconSetter.IsEmpty = slot.IsEmpty; 
                Debug.Log($"Добавлен предмет - {itemBase.ItemId} в инвентарь - {InventoryType} в слот {slotId}");
            }
        }
        
        public InventorySlot GetSlotById(int slotId)
        {
            return InventorySlotList.FirstOrDefault(slot => slot.SlotId == slotId);
        }

        public void AddIconSetter(Transform panel)
        {
            int slotCount = IconSetterList.Count;
            
            for (int i = 0; i < panel.childCount; i++)
            {
                var slot = panel.GetChild(i).GetComponent<UIItemIconSetter>();
                
                if (slot != null)
                {
                    IconSetterList.Add(slot);
                    IconSetterList[slotCount].SlotId = slotCount;
                    slotCount++;
                }
            }
        }
        
        public void AddSlot(Transform panel, bool isEquipment)
        {
            int slotCount = InventorySlotList.Count;
            
            for (int i = 0; i < panel.childCount; i++)
            {
                var slot = panel.GetChild(i).GetComponent<UIItemIconSetter>();
                
                if (slot != null)
                {
                    InventorySlotList.Add(new InventorySlot(slotCount, isEquipment));
                    IconSetterList.Add(slot);
                    IconSetterList[slotCount].SlotId = slotCount;
                    slotCount++;
                }
            }
        }

        public void AddItem(ItemBase itemBase)
        {
            if (!CheckInventoryFullness())
            {
                var emptySlot = InventorySlotList.Find(slot => slot.IsEmpty);
                
                if (emptySlot != null && !emptySlot.IsEquipmentSlot)
                {
                    emptySlot.ItemBase = itemBase;
                    emptySlot.IsEmpty = false;
                    var emptyIconSetter = IconSetterList[emptySlot.SlotId];
                    emptyIconSetter.SetIcon(itemBase.Icon);
                    emptyIconSetter.IsEmpty = emptySlot.IsEmpty;
                    OnAddItem.Invoke();
                    return;
                }
            }
            Debug.LogWarning($"Инвентарь - {InventoryType} полный");
        }

        private bool CheckInventoryFullness()
        {
            return InventorySlotList.All(slot => !slot.IsEmpty || slot.IsEquipmentSlot);
        }

        public void MoveItem(int oldSlotId, int newSlotId)
        {
            InventorySlot oldSlot = InventorySlotList.Find(s => s.SlotId == oldSlotId);
            InventorySlot newSlot = InventorySlotList.Find(s => s.SlotId == newSlotId);

            if (newSlot == oldSlot)
            {
                return;
            }

            if (oldSlot != null && newSlot != null && oldSlot.ItemBase != null)
            {
                Debug.Log($"Попытка перенести предмет - {oldSlot.ItemBase.ItemId} инвентаря - {InventoryType} из слота - {oldSlot.SlotId} в слот - {newSlot.SlotId}");

                // Если тип предмета Weapon, просто перемещаем его
                if (oldSlot.ItemBase is WeaponItemBase)
                {
                    if (newSlot.IsEquipmentSlot)
                    {
                        oldSlot.ItemBase.IsEquipment = true;
                    }
                    else
                    {
                        oldSlot.ItemBase.IsEquipment = false;
                    }
                    
                    Debug.Log($"Экипирован предмет {oldSlot.ItemBase.ItemId}");
                    OnEquippedItem.Invoke(GetAllEquipmentItems());
                    
                    if (newSlot.ItemBase != null)
                    {
                        // Если целевой слот не пустой, поменяем местами предметы
                        SwapItems(oldSlot, newSlot);
                    }
                    else
                    {
                        // Если целевой слот пустой, переместим предмет в этот слот
                        MoveItemToEmptySlot(oldSlot, newSlot);
                    }
                }
                else
                {
                    // Проверка на сумму Strength предмета и текущей суммы Strength
                    if ((oldSlot.ItemBase is SpellItemBase spellItem && spellItem.Strength > 0) || (oldSlot.ItemBase is ArtefactItemBase artefactItem && artefactItem.Strength > 0))
                    {
                        float itemStrength = (oldSlot.ItemBase as SpellItemBase)?.Strength ?? (oldSlot.ItemBase as ArtefactItemBase)?.Strength ?? 0;
                        float totalStrength = itemStrength + CurrentTotalStrength;

                        if (oldSlot.IsEquipmentSlot && !newSlot.IsEquipmentSlot)
                        {
                            if (totalStrength > TotalStrengthLimit && newSlot.IsEquipmentSlot)
                            {
                                Debug.LogWarning("Предмет не перемещен, превышен лимит общей силы");
                                return;
                            }
                            
                            // Если Strength не превышает лимит, добавляем его к currentTotalStrength
                            if (newSlot.IsEquipmentSlot)
                            {
                                CurrentTotalStrength = totalStrength;
                                oldSlot.ItemBase.IsEquipment = true;
                            }
                        }
                        
                        OnChangedStrengthInventory.Invoke();
                        OnEquippedItem.Invoke(GetAllEquipmentItems());
                        Debug.Log($"Экипирован предмет {oldSlot.ItemBase.ItemId}");
                    }
                    
                    // Проверка на удаление предмета из слота экипировки
                    if (!newSlot.IsEquipmentSlot && oldSlot.IsEquipmentSlot &&  oldSlot.ItemBase != null)
                    {
                        float itemStrength = (oldSlot.ItemBase as SpellItemBase)?.Strength ?? (oldSlot.ItemBase as ArtefactItemBase)?.Strength ?? 0;
                        CurrentTotalStrength -= itemStrength;
                        OnChangedStrengthInventory.Invoke();
                        oldSlot.ItemBase.IsEquipment = false;
                        
                        Debug.Log($"Снят предмет {oldSlot.ItemBase.ItemId}");
                    }
                    
                    if (newSlot.ItemBase != null)
                    {
                        // Если целевой слот не пустой, поменяем местами предметы
                        SwapItems(oldSlot, newSlot);
                    }
                    else
                    {
                        // Если целевой слот пустой, переместим предмет в этот слот
                        MoveItemToEmptySlot(oldSlot, newSlot);
                    }
                }
            }
        }

        private void SwapItems(InventorySlot oldSlot, InventorySlot newSlot)
        {
            (oldSlot.ItemBase, newSlot.ItemBase) = (newSlot.ItemBase, oldSlot.ItemBase);

            (oldSlot.IsEmpty, newSlot.IsEmpty) = (newSlot.IsEmpty, oldSlot.IsEmpty);

            UpdateIconSetter(oldSlot);
            UpdateIconSetter(newSlot);

            Debug.Log($"Предметы обменены. Предмет - {oldSlot.ItemBase.ItemId} из слота - {oldSlot.SlotId} перемещен в слот - {newSlot.SlotId}");
        }

        private void MoveItemToEmptySlot(InventorySlot oldSlot, InventorySlot newSlot)
        {
            newSlot.ItemBase = oldSlot.ItemBase;
            oldSlot.ItemBase = null;

            newSlot.IsEmpty = false;
            oldSlot.IsEmpty = true;

            UpdateIconSetter(newSlot); // Обновляем IconSetter для целевого (пустого) слота
            UpdateIconSetter(oldSlot);

            // Устанавливаем пустой спрайт для fromSlot (пустого слота)
            IconSetterList[oldSlot.SlotId].Icon.sprite = null;

            Debug.Log($"Предмет - {newSlot.ItemBase.ItemId} инвентаря - {InventoryType} перемещен из слота - {oldSlot.SlotId} в слот - {newSlot.SlotId}");
        }

        private void UpdateIconSetter(InventorySlot slot)
        {
            var iconSetter = IconSetterList[slot.SlotId];
            iconSetter.IsEmpty = slot.IsEmpty;

            if (slot.IsEmpty)
            {
                iconSetter.Icon.sprite = null; // Очищаем спрайт
                iconSetter.Icon.color = new Color(1, 1, 1, 0); // Делаем иконку прозрачной
            }
            else
            {
                iconSetter.SetIcon(slot.ItemBase.Icon); // Устанавливаем иконку предмета
            }
        }
    }
}
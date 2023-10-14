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
        
        private List<InventorySlot> _inventorySlotList = new();
        
        [JsonIgnore] private List<IconSetter> _iconSetterList = new();
        
        [field: JsonIgnore] public event Action<List<Item>> OnEquippedItem = delegate {  };
        [field: JsonIgnore] public event Action OnChangedStrengthInventory = delegate {  };
        [field: JsonIgnore] public event Action OnAddItem = delegate {  };
        
        public float TotalStrengthLimit { get; set; }
        public float CurrentTotalStrength { get; set; }

        public EItemType InventoryType { get; }

        public void Initialize(Transform panel, bool isEquipment)
        {
            AddSlot(panel, isEquipment);
        }

        public int GetCountSlotInventory()
        {
            return _inventorySlotList.Count;
        }

        public List<Item> GetAllEquipmentItems()
        {
            List<Item> list = new();
            
            foreach (var slot in _inventorySlotList)
            {
                if (slot.Item != null && slot.Item.IsEquipment)
                {
                    list.Add(slot.Item);
                    Debug.Log(slot.Item.IsEquipment);
                }
            }
            return list;
        }
        
        public void SetSlotById(Item item, int slotId)
        {
            var slot = _inventorySlotList.Find(s => s.SlotId == slotId);

            if (slot != null)
            { 
                slot.Item = item;
                slot.IsEmpty = false;
                var iconSetter = _iconSetterList[slotId];
                iconSetter.SetIcon(item.Icon);
                iconSetter.IsEmpty = slot.IsEmpty; 
                Debug.Log($"Добавлен предмет - {item.ItemId} в инвентарь - {InventoryType} в слот {slotId}");
            }
        }
        
        public InventorySlot GetSlotById(int slotId)
        {
            return _inventorySlotList.FirstOrDefault(slot => slot.SlotId == slotId);
        }

        private void AddSlot(Transform panel, bool isEquipment)
        {
            int slotCount = _inventorySlotList.Count;
            
            for (int i = 0; i < panel.childCount; i++)
            {
                var slot = panel.GetChild(i).GetComponent<IconSetter>();
                
                if (slot != null)
                {
                    _inventorySlotList.Add(new InventorySlot(slotCount, isEquipment));
                    _iconSetterList.Add(slot);
                    _iconSetterList[slotCount].SlotId = slotCount;
                    slotCount++;
                }
            }
        }

        public void AddItem(Item item)
        {
            if (!CheckInventoryFullness())
            {
                var emptySlot = _inventorySlotList.Find(slot => slot.IsEmpty);
                
                if (emptySlot != null && !emptySlot.IsEquipmentSlot)
                {
                    emptySlot.Item = item;
                    emptySlot.IsEmpty = false;
                    var emptyIconSetter = _iconSetterList[emptySlot.SlotId];
                    emptyIconSetter.SetIcon(item.Icon);
                    emptyIconSetter.IsEmpty = emptySlot.IsEmpty;
                    OnAddItem.Invoke();
                    return;
                }
            }
            Debug.LogWarning($"Инвентарь - {InventoryType} полный");
        }

        private bool CheckInventoryFullness()
        {
            return _inventorySlotList.All(slot => !slot.IsEmpty || slot.IsEquipmentSlot);
        }

        public void MoveItem(int oldSlotId, int newSlotId)
        {
            InventorySlot oldSlot = _inventorySlotList.Find(s => s.SlotId == oldSlotId);
            InventorySlot newSlot = _inventorySlotList.Find(s => s.SlotId == newSlotId);

            if (newSlot == oldSlot)
            {
                return;
            }

            if (oldSlot != null && newSlot != null && oldSlot.Item != null)
            {
                Debug.Log($"Попытка перенести предмет - {oldSlot.Item.ItemId} инвентаря - {InventoryType} из слота - {oldSlot.SlotId} в слот - {newSlot.SlotId}");

                // Если тип предмета Weapon, просто перемещаем его
                if (oldSlot.Item is WeaponItem)
                {
                    if (newSlot.IsEquipmentSlot)
                    {
                        oldSlot.Item.IsEquipment = true;
                    }
                    else
                    {
                        oldSlot.Item.IsEquipment = false;
                    }
                    
                    Debug.Log($"Экипирован предмет {oldSlot.Item.ItemId}");
                    OnEquippedItem.Invoke(GetAllEquipmentItems());
                    
                    if (newSlot.Item != null)
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
                    if ((oldSlot.Item is SpellItem spellItem && spellItem.Strength > 0) || (oldSlot.Item is ArtefactItem artefactItem && artefactItem.Strength > 0))
                    {
                        float itemStrength = (oldSlot.Item as SpellItem)?.Strength ?? (oldSlot.Item as ArtefactItem)?.Strength ?? 0;
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
                                oldSlot.Item.IsEquipment = true;
                            }
                        }
                        
                        OnChangedStrengthInventory.Invoke();
                        OnEquippedItem.Invoke(GetAllEquipmentItems());
                        Debug.Log($"Экипирован предмет {oldSlot.Item.ItemId}");
                    }
                    
                    // Проверка на удаление предмета из слота экипировки
                    if (!newSlot.IsEquipmentSlot && oldSlot.IsEquipmentSlot &&  oldSlot.Item != null)
                    {
                        float itemStrength = (oldSlot.Item as SpellItem)?.Strength ?? (oldSlot.Item as ArtefactItem)?.Strength ?? 0;
                        CurrentTotalStrength -= itemStrength;
                        OnChangedStrengthInventory.Invoke();
                        oldSlot.Item.IsEquipment = false;
                        
                        Debug.Log($"Снят предмет {oldSlot.Item.ItemId}");
                    }
                    
                    if (newSlot.Item != null)
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
            (oldSlot.Item, newSlot.Item) = (newSlot.Item, oldSlot.Item);

            (oldSlot.IsEmpty, newSlot.IsEmpty) = (newSlot.IsEmpty, oldSlot.IsEmpty);

            UpdateIconSetter(oldSlot);
            UpdateIconSetter(newSlot);

            Debug.Log($"Предметы обменены. Предмет - {oldSlot.Item.ItemId} из слота - {oldSlot.SlotId} перемещен в слот - {newSlot.SlotId}");
        }

        private void MoveItemToEmptySlot(InventorySlot oldSlot, InventorySlot newSlot)
        {
            newSlot.Item = oldSlot.Item;
            oldSlot.Item = null;

            newSlot.IsEmpty = false;
            oldSlot.IsEmpty = true;

            UpdateIconSetter(newSlot); // Обновляем IconSetter для целевого (пустого) слота
            UpdateIconSetter(oldSlot);

            // Устанавливаем пустой спрайт для fromSlot (пустого слота)
            _iconSetterList[oldSlot.SlotId].Icon.sprite = null;

            Debug.Log($"Предмет - {newSlot.Item.ItemId} инвентаря - {InventoryType} перемещен из слота - {oldSlot.SlotId} в слот - {newSlot.SlotId}");
        }

        private void UpdateIconSetter(InventorySlot slot)
        {
            var iconSetter = _iconSetterList[slot.SlotId];
            iconSetter.IsEmpty = slot.IsEmpty;

            if (slot.IsEmpty)
            {
                iconSetter.Icon.sprite = null; // Очищаем спрайт
                iconSetter.Icon.color = new Color(1, 1, 1, 0); // Делаем иконку прозрачной
            }
            else
            {
                iconSetter.SetIcon(slot.Item.Icon); // Устанавливаем иконку предмета
            }
        }
    }
}
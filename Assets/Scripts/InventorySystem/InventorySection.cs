using System;
using System.Collections.Generic;
using System.Linq;
using ItemSystem;
using Newtonsoft.Json;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

    [field: JsonIgnore] public event Action<List<ItemBase>> OnEquippedItem = delegate { };
    [field: JsonIgnore] public event Action OnChangedStrengthInventory = delegate { };
    [field: JsonIgnore] public event Action OnAddItem = delegate { };

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

    public void EquipOrUnequipItem(int oldSlotId, int newSlotId)
    {
        InventorySlot oldSlot = InventorySlotList.Find(s => s.SlotId == oldSlotId);
        InventorySlot newSlot = InventorySlotList.Find(s => s.SlotId == newSlotId);

        Debug.Log($"Попытка обработать предмет - {oldSlot.ItemBase?.ItemId} из слота - {oldSlot.SlotId} в слот - {newSlot.SlotId}");

        if (oldSlot.ItemBase is WeaponItemBase)
        {
            if (!newSlot.IsEmpty)
            {
                SwapItems(oldSlot, newSlot);
                Debug.Log($"Предмет - {newSlot.ItemBase?.ItemId} обменен с предметом - {oldSlot.ItemBase?.ItemId}");
                MoveUnequippedItemsToTop();
            }
            else
            {
                if (newSlot.IsEmpty)
                {
                    MoveItemToEmptySlot(oldSlot, newSlot);
                    Debug.Log($"Предмет - {oldSlot.ItemBase?.ItemId} экипирован из слота - {newSlot.SlotId}");
                    MoveUnequippedItemsToTop();
                }
            }
        }
        else
        {
            float itemStrength;
            float equippedItemStrength = 0;
            if (!newSlot.IsEmpty)
            {
                equippedItemStrength = (newSlot.ItemBase as SpellItemBase)?.Strength ?? (newSlot.ItemBase as ArtefactItemBase)?.Strength ?? 0;
                itemStrength = (newSlot.ItemBase as SpellItemBase)?.Strength ?? (newSlot.ItemBase as ArtefactItemBase)?.Strength ?? 0;
            }
            else
            {
                equippedItemStrength = (oldSlot.ItemBase as SpellItemBase)?.Strength ?? (oldSlot.ItemBase as ArtefactItemBase)?.Strength ?? 0;
                itemStrength = (oldSlot.ItemBase as SpellItemBase)?.Strength ?? (oldSlot.ItemBase as ArtefactItemBase)?.Strength ?? 0;
            }

            float equippedItemTotalStrength = equippedItemStrength + CurrentTotalStrength;
            
            if (!newSlot.IsEquipmentSlot)
            {
                if (newSlot.IsEmpty)
                {
                    MoveItemToEmptySlot(oldSlot, newSlot);
                    CurrentTotalStrength -= itemStrength;
                    OnChangedStrengthInventory.Invoke();
                    MoveUnequippedItemsToTop();
                    return;
                }
                if (equippedItemTotalStrength > TotalStrengthLimit) 
                {
                    Debug.LogWarning("Предмет не экипирован, превышен лимит общей силы оборудования");
                    return;
                }
                SwapItems(oldSlot, newSlot);
                CurrentTotalStrength -= equippedItemStrength;
                Debug.LogError(CurrentTotalStrength);
                OnChangedStrengthInventory.Invoke();
                MoveUnequippedItemsToTop();
                return;
            }
            if (newSlot.IsEmpty)
            {
                if (equippedItemTotalStrength > TotalStrengthLimit)
                {
                    Debug.LogWarning("Предмет не экипирован, превышен лимит общей силы оборудования");
                    return;
                }
                MoveItemToEmptySlot(oldSlot, newSlot);
                CurrentTotalStrength += equippedItemStrength;
                OnChangedStrengthInventory.Invoke();
                Debug.Log($"Предмет - {oldSlot.ItemBase?.ItemId} экипирован из слота - {newSlot.SlotId}");
                MoveUnequippedItemsToTop();
            }
            else
            {
                if (equippedItemTotalStrength > TotalStrengthLimit)
                {
                    Debug.LogWarning("Предмет не экипирован, превышен лимит общей силы оборудования");
                    return;
                }
                SwapItems(oldSlot, newSlot);
                CurrentTotalStrength += equippedItemTotalStrength;
                OnChangedStrengthInventory.Invoke();
                Debug.Log($"Предмет - {newSlot.ItemBase?.ItemId} обменен с предметом - {oldSlot.ItemBase?.ItemId}");
                MoveUnequippedItemsToTop();
            }
        }
    }
    
    public void MoveUnequippedItemsToTop()
    {
        InventorySlot firstFreeSlot = null;

        foreach (var slot in InventorySlotList)
        {
            if (!slot.IsEquipmentSlot && slot.IsEmpty && firstFreeSlot == null)
            {
                firstFreeSlot = slot;
            }
        }

        foreach (var slot in InventorySlotList)
        {
            if (!slot.IsEquipmentSlot && !slot.IsEmpty)
            {
                if (firstFreeSlot != null && firstFreeSlot.SlotId < slot.SlotId)
                {
                    // Переместить предмет из текущего занятого слота в первый свободный
                    MoveItemToEmptySlot(slot, firstFreeSlot);

                    // Обновить ссылку на первый свободный слот, чтобы продолжить сортировку
                    firstFreeSlot = InventorySlotList.Find(s => s.SlotId == firstFreeSlot.SlotId);

                    if (firstFreeSlot == null)
                    {
                        // Все свободные слоты заполнены, прекратить сортировку
                        return;
                    }
                }
            }
        }
    }

    private bool SwapItems(InventorySlot slot1, InventorySlot slot2)
    {
        if (slot1 == null || slot2 == null || slot1.ItemBase == null || slot2.ItemBase == null)
        {
            return false;
        }

        float strength1 = (slot1.ItemBase as SpellItemBase)?.Strength ?? (slot1.ItemBase as ArtefactItemBase)?.Strength ?? 0;
        float strength2 = (slot2.ItemBase as SpellItemBase)?.Strength ?? (slot2.ItemBase as ArtefactItemBase)?.Strength ?? 0;

        float totalStrength1 = CurrentTotalStrength - strength1 + strength2;
        float totalStrength2 = CurrentTotalStrength - strength2 + strength1;

        if (totalStrength1 > TotalStrengthLimit || totalStrength2 > TotalStrengthLimit)
        {
            Debug.LogWarning("Предметы не могут быть обменены из-за превышения лимита силы");
            return false;
        }

        (slot1.ItemBase, slot2.ItemBase) = (slot2.ItemBase, slot1.ItemBase);
        (slot1.IsEmpty, slot2.IsEmpty) = (slot2.IsEmpty, slot1.IsEmpty);

        UpdateIconSetter(slot1);
        UpdateIconSetter(slot2);

        return true;
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

        Debug.Log(
            $"Предмет - {newSlot.ItemBase.ItemId} инвентаря - {InventoryType} перемещен из слота - {oldSlot.SlotId} в слот - {newSlot.SlotId}");
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
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Debug = UnityEngine.Debug;

[Serializable]
public class InventorySection
{
    public InventorySection(
        EItemType inventoryType, 
        float totalStrengthLimit, 
        float currentStrength, 
        List<InventorySlot> inventorySlotList)
    {
        InventoryType = inventoryType;
        InventorySlotList = new List<InventorySlot>(inventorySlotList);
        CurrentStrength = currentStrength;
        TotalStrengthLimit = totalStrengthLimit;
    }

    public List<InventorySlot> InventorySlotList { get; set; }

    [field: JsonIgnore] public event Action<SlotTransferInfo> OnItemMoved;
    [field: JsonIgnore] public event Action OnChangedStrengthInventory;

    public float TotalStrengthLimit { get; set; }
    public float CurrentStrength { get; set; }

    public EItemType InventoryType { get; }

    public List<InventorySlot> GetAllNotEmptyInventorySlots()
    {
        return new List<InventorySlot>(InventorySlotList.FindAll(slot => slot.IsEmpty == false));
    }
    
    public List<InventorySlot> GetInventorySlotList()
    {
        return new List<InventorySlot>(InventorySlotList);
    }

    public List<string> GetAllEquipmentInventoryItems()
    {
        List<string> list = new();
        
        foreach (var slot in InventorySlotList)
        {
            if (slot.IsEquipment)
            {
                list.Add(slot.ItemId);
            }
        }
        return list;
    }

    public InventorySlot GetEquippedSlotByItemId(string itemId)
    {
        for (int i = 0; i < InventorySlotList.Count; i++)
        {
            if (InventorySlotList[i].ItemId == itemId && InventorySlotList[i].IsEquipment)
            {
                return InventorySlotList[i];
            }
        }
        return null;
    }

    public InventorySlot GetSlotBySlotId(int slotId)
    {
        return InventorySlotList.FirstOrDefault(slot => slot.SlotId == slotId);
    }

    public void MoveItem(ItemBase item, int slotId)
    {
        foreach (var inventorySlot in InventorySlotList)
        {
            if (inventorySlot.SlotId == slotId)
            {
                if (inventorySlot.ItemId == null)
                {;
                    return;
                }
                break;
            }
        }
        
        InventorySlot slot = InventorySlotList.Find(slot => slot.SlotId == slotId);
        
        
        if (item as ArtefactItemBase || item as SpellItemBase)
        {
            if (slot.IsEquipment)
            {
                RemoveItem(item, slotId);
                return;
            }
            EquipItem(item, slotId);
            return;
        }

        if (!slot.IsEquipment)
        {
            SwapItems(item);
        }
    }

    private void SwapItems(ItemBase item)
    {
        InventorySlot standardSlot = InventorySlotList.Find(slot => slot.ItemId == item.ItemId);
        InventorySlot equipSlot = InventorySlotList.Find(slot => slot.IsEquipment);
        
        Debug.Log($"Попытка поменять предметы - {standardSlot.ItemId} и {equipSlot.ItemId} местами");

        if (!item.IsLocked)
        {
            equipSlot.ItemId = standardSlot.ItemId;
            
            OnItemMoved?.Invoke(new SlotTransferInfo(standardSlot.SlotId, equipSlot.SlotId, InventoryType, true));
            Debug.Log($"Предметы поменяны местами");
        }
        Debug.LogWarning("Предмет заблокирован");
    }

    private void RemoveItem(ItemBase item, int slotId)
    {
        InventorySlot standardSlot = InventorySlotList.Find(slot => slot.ItemId == item.ItemId);
        InventorySlot equipSlot = InventorySlotList.Find(slot => slot.SlotId == slotId);
        
        Debug.Log($"Попытка снять предмет - {item.ItemId}");
        SetItemValues(item, equipSlot, false);
        CurrentStrength -= GetItemStrength(item);
        
        OnItemMoved?.Invoke(new SlotTransferInfo(standardSlot.SlotId, equipSlot.SlotId, InventoryType, false));
        OnChangedStrengthInventory?.Invoke();
        Debug.Log($"Предмет снят");
    }

    private void EquipItem(ItemBase item, int slotId)
    {
        InventorySlot standardSlot = InventorySlotList.Find(slot => slot.SlotId == slotId);
        InventorySlot equipSlot = GetFirstEmptyEquipmentSlot();
        
        Debug.Log($"Попытка экипировать предмет - {item.ItemId}");

        if (!item.IsLocked)
        {
            if (!item.IsEquipment)
            {
                if (GetFirstEmptyEquipmentSlot() != null)
                {
                    SetItemValues(item, equipSlot, true);
                    float equippedItemTotalStrength = GetItemStrength(item) + CurrentStrength;

                    if (equippedItemTotalStrength > TotalStrengthLimit)
                    {
                        Debug.LogWarning("Предмет не экипирован, превышен лимит общей силы");
                        return;
                    }

                    CurrentStrength += GetItemStrength(item);
                    Debug.Log($"Предмет - {item.ItemId} экипирован");
                    OnItemMoved?.Invoke(
                        new SlotTransferInfo(standardSlot.SlotId, equipSlot.SlotId, InventoryType, true));
                    OnChangedStrengthInventory?.Invoke();
                    return;
                }
                Debug.LogWarning("Слоты экипировки заняты");
                return;
            }
            Debug.LogWarning("Предмет уже экипирован");
            return;
        }
        Debug.LogWarning("Предмет заблокирован");
    }

    private void SetItemValues(ItemBase item, InventorySlot slot, bool isEquip)
    {
        item.IsEquipment = isEquip;
        slot.IsEmpty = !isEquip;
        slot.ItemId = isEquip ? item.ItemId : null;
    }

    public string GetItemIdBySlotId(int slotId)
    {
        InventorySlot slot = InventorySlotList.Find(slot => slot.SlotId == slotId);
        
        if (slot.ItemId != null)
        {
            return slot.ItemId;
        }

        return null;
    }

    private InventorySlot GetFirstEmptyEquipmentSlot()
    {
        foreach (var slot in InventorySlotList)
        {
            if (slot.IsEmpty && slot.IsEquipment)
            {
                return slot;
            }
        }

        return null;
    }

    private float GetItemStrength(ItemBase item)
    {
        return (item as SpellItemBase)?.Strength ?? (item as ArtefactItemBase)?.Strength ?? 0;
    }
}

public class SlotTransferInfo
{
    public SlotTransferInfo(int standardSlotId, int equipSlotId, EItemType inventoryType, bool isEquipment)
    {
        StandardSlotId = standardSlotId;
        EquipSlotId = equipSlotId;
        InventoryType = inventoryType;
        IsEquipment = isEquipment;
    }
    
    public readonly EItemType InventoryType;
    public readonly bool IsEquipment;
    public readonly int StandardSlotId;
    public readonly int EquipSlotId;
}
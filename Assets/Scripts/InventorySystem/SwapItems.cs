using UnityEngine;
using Zenject;

public class SwapItems : MonoBehaviour
{
    [Inject] private InventoryManager _inventoryManager;
    private int _equpSlot = -1;
    private int _defSlot = -1;
    private bool _isEquipmentSlot;

    public void ExchangeSlotData(int slotId)
    {
        var inventorySection = _inventoryManager.inventory.Sections[_inventoryManager.currentInventorySection];
        
        if (inventorySection.GetSlotById(slotId).IsEmpty && (_equpSlot == -1 && _defSlot == -1)) return;
        
        _isEquipmentSlot = inventorySection.GetSlotById(slotId).IsEquipmentSlot;
        
        if (_isEquipmentSlot)
        {
            _equpSlot = slotId;
        }
        else
        {
            _defSlot = slotId;
        }

        if (_equpSlot != -1 && _defSlot != -1 && 
            (!inventorySection.GetSlotById(_equpSlot).IsEmpty || !inventorySection.GetSlotById(_defSlot).IsEmpty))
        {
            if (inventorySection.GetSlotById(_defSlot).IsEmpty)
            {
                inventorySection.EquipOrUnequipItem(_equpSlot, _defSlot);
            }
            else
            {
                inventorySection.EquipOrUnequipItem(_defSlot, _equpSlot);
            }
            ResetSelectedItems();
        }
    }

    public void ResetSelectedItems()
    {
        Debug.LogWarning("Внимание [SwapItems] - выбранные предметы были сброшены");
        _equpSlot = -1;
        _defSlot = -1;
    }

}

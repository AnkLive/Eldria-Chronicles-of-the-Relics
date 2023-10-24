using UnityEngine;

public class SwapItems : MonoBehaviour
{
    public InventoryManager _InventoryManager;
    private int EqupSlot = -1; // Используем -1, чтобы указать, что слот не выбран
    private int DefSlot = -1;  // Используем -1, чтобы указать, что слот не выбран
    private bool isEquipmentSlot;

    public void ExchangeSlotData(int slotId)
    {
        if (_InventoryManager.inventory.Sections[_InventoryManager.currentInventorySection].GetSlotById(slotId).IsEmpty && (EqupSlot == -1 && DefSlot == -1)) return;
        
        isEquipmentSlot = _InventoryManager.inventory.Sections[_InventoryManager.currentInventorySection].GetSlotById(slotId).IsEquipmentSlot;

        if (isEquipmentSlot)
        {
            EqupSlot = slotId;
        }
        else
        {
            DefSlot = slotId;
        }

        // Проверить, что оба слота не равны -1 (не выбраны) и не пусты, и вызвать метод, когда они оба заполнены и не пусты
        if (EqupSlot != -1 && DefSlot != -1 && 
            (!_InventoryManager.inventory.Sections[_InventoryManager.currentInventorySection].GetSlotById(EqupSlot).IsEmpty || 
             !_InventoryManager.inventory.Sections[_InventoryManager.currentInventorySection].GetSlotById(DefSlot).IsEmpty))
        {
            if (_InventoryManager.inventory.Sections[_InventoryManager.currentInventorySection].GetSlotById(DefSlot).IsEmpty)
            {
                _InventoryManager.inventory.Sections[_InventoryManager.currentInventorySection].EquipOrUnequipItem(EqupSlot, DefSlot);
            }
            else
            {
                _InventoryManager.inventory.Sections[_InventoryManager.currentInventorySection].EquipOrUnequipItem(DefSlot, EqupSlot);
            }
            // Сбросить значения слотов после использования
            EqupSlot = -1;
            DefSlot = -1;
        }
    }

}

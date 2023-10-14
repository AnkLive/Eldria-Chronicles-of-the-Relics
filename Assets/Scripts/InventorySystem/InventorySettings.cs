using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Inventory Settings", menuName = "Inventory/Inventory Settings", order = 0)]
    public class InventorySettings : ScriptableObject
    {
        public bool canUseInventory = true; // Возможность использовать инвентарь
        public bool canUseQuickSlots = true; // Возможность использовать слоты быстрого доступа
        public bool canUseEquipmentSlots = true; // Возможность использовать слоты под одежду
        public bool hideQuickSlotPanel = false; // Скрывать панель быстрого доступа
        public bool canMoveItemsInInventory = true; // Возможность перемещать предметы в инвентаре

        private void Awake()
        {
            canMoveItemsInInventory = canUseInventory && canMoveItemsInInventory;
            hideQuickSlotPanel = canUseInventory && hideQuickSlotPanel;
        }
    }
}
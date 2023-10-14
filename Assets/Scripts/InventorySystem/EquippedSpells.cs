using System.Collections.Generic;
using ItemSystem;
using UnityEngine;

namespace InventorySystem
{
    public class EquippedSpells : MonoBehaviour
    {
        public InventoryManager inventoryManager;
        public GlobalStringVars stringVars;
        public ItemStorage itemStorage;
        public List<string> currentEquipmentSpells = new();
        public IconSetter iconSetter;

        public void Initialize()
        {
            inventoryManager.inventory.Sections[EItemType.Spell].OnEquippedItem += SetCurrentEquipmentSpells;
        }

        private void SetCurrentEquipmentSpells(List<Item> listId)
        {
            List<string> list = new List<string>();
            
            foreach (var id in listId)
            {
                list.Add(id.ItemId);
            }
            currentEquipmentSpells = list;
        }

        private void Update()
        {
            SwapCurrentSpell();
        }

        private void SwapCurrentSpell()
        {
            if (Input.GetKeyDown(stringVars.GetVars("FIRST_SPELL_SLOT")) && !string.IsNullOrEmpty(currentEquipmentSpells[0]))
            {
                iconSetter.SetIcon(itemStorage.GetItemDescriptionById(currentEquipmentSpells[0]).sprite);
                Debug.Log($"Выбрано заклинание - {currentEquipmentSpells[0]}");
                return;
            }
            if (Input.GetKeyDown(stringVars.GetVars("TWO_SPELL_SLOT")) && !string.IsNullOrEmpty(currentEquipmentSpells[1]))
            {
                iconSetter.SetIcon(itemStorage.GetItemDescriptionById(currentEquipmentSpells[1]).sprite);
                Debug.Log($"Выбрано заклинание - {currentEquipmentSpells[1]}");
                return;
            }
            if (Input.GetKeyDown(stringVars.GetVars("THREE_SPELL_SLOT")) && !string.IsNullOrEmpty(currentEquipmentSpells[2]))
            {
                iconSetter.SetIcon(itemStorage.GetItemDescriptionById(currentEquipmentSpells[2]).sprite);
                Debug.Log($"Выбрано заклинание - {currentEquipmentSpells[2]}");
                return;
            }
        }
    }
}

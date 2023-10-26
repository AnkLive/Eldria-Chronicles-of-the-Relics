using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

    public class SpellEquipmentManager : MonoBehaviour
    {
        public InventoryManager inventoryManager;
        public StringVariableManager stringVariableManager;
        public ItemStorage itemStorage;
        public List<string> currentEquipmentSpells = new();
        [FormerlySerializedAs("iconSetter")] public UIItemIconSetter uiItemIconSetter;

        public void Initialize()
        {
            inventoryManager.inventory.Sections[EItemType.Spell].OnEquippedItem += SetCurrentEquipmentSpells;
        }

        private void SetCurrentEquipmentSpells(List<ItemBase> listId)
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
            //SwapCurrentSpell();
        }

        // private void SwapCurrentSpell()
        // {
        //     if (Input.GetKeyDown(stringVariableManager.GetVars("FIRST_SPELL_SLOT")) && !string.IsNullOrEmpty(currentEquipmentSpells[0]))
        //     {
        //         uiItemIconSetter.SetIcon(itemStorage.GetItemDescriptionById(currentEquipmentSpells[0]).sprite);
        //         Debug.Log($"Выбрано заклинание - {currentEquipmentSpells[0]}");
        //         return;
        //     }
        //     if (Input.GetKeyDown(stringVariableManager.GetVars("TWO_SPELL_SLOT")) && !string.IsNullOrEmpty(currentEquipmentSpells[1]))
        //     {
        //         uiItemIconSetter.SetIcon(itemStorage.GetItemDescriptionById(currentEquipmentSpells[1]).sprite);
        //         Debug.Log($"Выбрано заклинание - {currentEquipmentSpells[1]}");
        //         return;
        //     }
        //     if (Input.GetKeyDown(stringVariableManager.GetVars("THREE_SPELL_SLOT")) && !string.IsNullOrEmpty(currentEquipmentSpells[2]))
        //     {
        //         uiItemIconSetter.SetIcon(itemStorage.GetItemDescriptionById(currentEquipmentSpells[2]).sprite);
        //         Debug.Log($"Выбрано заклинание - {currentEquipmentSpells[2]}");
        //         return;
        //     }
        // }
    }

using System;
using ItemSystem;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public class InventoryManager : MonoBehaviour, IInitialize<InventoryManager>
    {
        public InventorySaveLoader inventorySaveLoader;
        [SerializeField] public InventoryUIManager UIManager;
        [SerializeField] private EquippedSpells EquippedSpells;
        public Item testItemSpell;
        public Item testItemWeapon;
        public Item testItemArtefact;
        [SerializeField] private ItemStorage itemStorage;
        public Inventory inventory;
        public EItemType currentInventorySection = EItemType.Weapon;
        [SerializeField] private float startTotalStrengthLimitSpellSection;
        [SerializeField] private float startTotalStrengthLimitArtefactSection;
        public bool isCheckpoint;
        
        public void Initialize()
        {
            UIManager.OpenInventory();
            UIManager.Init();
            
            inventory = new Inventory(itemStorage);
            inventory.InitializeInventorySections(UIManager.inventoryPanel, UIManager.equipmentPanel,  startTotalStrengthLimitArtefactSection, startTotalStrengthLimitSpellSection);
            inventory.LoadInventoryItems(inventorySaveLoader);

            UIManager.OnCloseInventory += SaveInventory;
            UIManager.OnOpenInventory += SaveInventory;
            UIManager.OnChangeInventorySection += SetCurrentInventorySection;
            
            UIManager.SetInventoryStrength(inventory.Sections[EItemType.Artefact].CurrentTotalStrength);
            UIManager.SetInventoryStrength(inventory.Sections[EItemType.Spell].CurrentTotalStrength);
            
            inventory.Sections[EItemType.Artefact].OnChangedStrengthInventory += GetStrengthInventory;
            inventory.Sections[EItemType.Spell].OnChangedStrengthInventory += GetStrengthInventory;
            
            UIManager.CloseInventory();
            Debug.LogWarning("Инвентарь загружен");
            
            EquippedSpells.Initialize();
        }

        private void GetStrengthInventory()
        {
            UIManager.SetInventoryStrength(inventory.Sections[currentInventorySection].CurrentTotalStrength);
        }

        private void OnDisable()
        {
            UIManager.OnChangeInventorySection -= SetCurrentInventorySection;
            inventory.Sections[EItemType.Artefact].OnChangedStrengthInventory -= GetStrengthInventory;
            inventory.Sections[EItemType.Spell].OnChangedStrengthInventory -= GetStrengthInventory;
        }
        
        public void SetCurrentInventorySection(EItemType section)
        {
            currentInventorySection = section;
            UIManager.SetInventoryStrength(inventory.Sections[currentInventorySection].CurrentTotalStrength);
        }

        private void Update()
        {
            HandleInput();
        }
        
        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                inventory.AddItem(testItemSpell);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                inventory.AddItem(testItemWeapon);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                inventory.AddItem(testItemArtefact);
                return;
            }
        }

        private void SaveInventory()
        {
            inventorySaveLoader.SetData(inventory);
        }

        public void SetCurrentSelectedItem(int slotId)
        {
            if (inventory.Sections[currentInventorySection].GetSlotById(slotId).Item != null)
            {
                UIManager.SetItemDescriptionText(itemStorage.GetItemDescriptionById(inventory.Sections[currentInventorySection].GetSlotById(slotId).Item.ItemId).description);
            }
        }
    }
}
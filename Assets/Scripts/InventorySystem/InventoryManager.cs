using System;
using ItemSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace InventorySystem
{
    [Serializable]
    public class InventoryManager : MonoBehaviour, IInitialize<InventoryManager>
    {
        [Inject]
        private ISaveLoader<Inventory> _inventorySaveLoader;
        [SerializeField] public InventoryUIManager UIManager;
        [SerializeField] private SpellEquipmentManager spellEquipmentManager;
        public ItemBase testItemBaseSpell;
        public ItemBase testItemBaseWeapon;
        public ItemBase testItemBaseArtefact;
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
            
            inventory = new Inventory
            {
                itemStorage = itemStorage
            };
            inventory.InitializeInventorySections
                (
                    _inventorySaveLoader.GetData(), 
                    UIManager.inventoryPanel, 
                    UIManager.equipmentPanel,  
                    startTotalStrengthLimitArtefactSection, 
                    startTotalStrengthLimitSpellSection
                    );

            UIManager.OnCloseInventory += SaveInventory;
            UIManager.OnOpenInventory += SaveInventory;
            UIManager.OnChangeInventorySection += SetCurrentInventorySection;
            
            UIManager.SetInventoryStrength(inventory.Sections[EItemType.Artefact].CurrentTotalStrength);
            UIManager.SetInventoryStrength(inventory.Sections[EItemType.Spell].CurrentTotalStrength);
            
            inventory.Sections[EItemType.Artefact].OnChangedStrengthInventory += GetStrengthInventory;
            inventory.Sections[EItemType.Spell].OnChangedStrengthInventory += GetStrengthInventory;
            
            UIManager.CloseInventory();
            Debug.LogWarning("Инвентарь загружен");
            
            spellEquipmentManager.Initialize();
        }

        private void GetStrengthInventory()
        {
            UIManager.SetInventoryStrength(inventory.Sections[currentInventorySection].CurrentTotalStrength);
        }

        // private void OnDisable()
        // {
        //     UIManager.OnChangeInventorySection -= SetCurrentInventorySection;
        //     inventory.Sections[EItemType.Artefact].OnChangedStrengthInventory -= GetStrengthInventory;
        //     inventory.Sections[EItemType.Spell].OnChangedStrengthInventory -= GetStrengthInventory;
        // }
        
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
                inventory.AddItem(testItemBaseSpell);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                inventory.AddItem(testItemBaseWeapon);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                inventory.AddItem(testItemBaseArtefact);
                return;
            }
        }

        private void SaveInventory()
        {
            _inventorySaveLoader.SetData(inventory);
        }

        public void SetCurrentSelectedItem(int slotId)
        {
            if (inventory.Sections[currentInventorySection].GetSlotById(slotId).ItemBase != null)
            {
                UIManager.SetItemDescriptionText(itemStorage.GetItemDescriptionById(inventory.Sections[currentInventorySection].GetSlotById(slotId).ItemBase.ItemId).description);
            }
        }
    }
}
using System;
using UnityEngine;
using Zenject;

[Serializable]
public class InventoryManager : MonoBehaviour, IInitialize<InventoryManager>, IActivate<InventoryManager>
{
    public ItemBase testItemBaseSpell;
    public ItemBase testItemBaseSpell1;
    public ItemBase testItemBaseWeapon1;
    public ItemBase testItemBaseWeapon2;
    public ItemBase testItemBaseArtefact;
    public ItemBase testItemBaseArtefact1;
    
    private Controller _controller;
    [Inject] private ISaveLoader<Inventory> _inventorySaveLoader;
    [Inject] private InventoryUIManager _uIManager;
    [SerializeField] private SpellEquipmentManager spellEquipmentManager;
    [SerializeField] private ItemStorage itemStorage;
    public Inventory inventory;
    public EItemType currentInventorySection = EItemType.Weapon;
    [SerializeField] private float startTotalStrengthLimitSpellSection;
    [SerializeField] private float startTotalStrengthLimitArtefactSection;
    public bool isCheckpoint;
    
    public event Action OnInventoryLoaded;
    
    public void Activate()
    {
        _controller.Enable();
        
        _controller.Main.AddWeaponItem.performed += _ => AddWeaponItem1();
        _controller.Main.AddWeaponItem1.performed += _ => AddWeaponItem2();
        _controller.Main.AddArtefactItem.performed += _ => AddArtefactItem();
        _controller.Main.AddSpellItem.performed += _ => AddSpellItem();
        _controller.Main.AddArtefactItem1.performed += _ => AddArtefactItem1();
        _controller.Main.AddSpellItem1.performed += _ => AddSpellItem1();
        
        _uIManager.OnCloseInventory += SaveInventory;
        _uIManager.OnOpenInventory += SaveInventory;
        _uIManager.OnChangeInventorySection += SetCurrentInventorySection;
        
        inventory.Sections[EItemType.Artefact].OnChangedStrengthInventory += GetStrengthInventory;
        inventory.Sections[EItemType.Spell].OnChangedStrengthInventory += GetStrengthInventory;
        _uIManager.VisibleAllObjects(false);
    }

    public void Deactivate()
    {
        _controller.Disable();
        
        _uIManager.OnCloseInventory -= SaveInventory;
        _uIManager.OnOpenInventory -= SaveInventory;
        _uIManager.OnChangeInventorySection -= SetCurrentInventorySection;
        inventory.Sections[EItemType.Artefact].OnChangedStrengthInventory -= GetStrengthInventory;
        inventory.Sections[EItemType.Spell].OnChangedStrengthInventory -= GetStrengthInventory;
    }
    
    public void Initialize()
    {
        _controller = new Controller();
        inventory = new Inventory
        {
            itemStorage = itemStorage
        };
        
        inventory.InitializeInventorySections
            (
                _inventorySaveLoader.GetData(), 
                _uIManager.inventoryPanel, 
                _uIManager.equipmentPanel,  
                startTotalStrengthLimitArtefactSection, 
                startTotalStrengthLimitSpellSection
                );
        
        _uIManager.SetInventoryStrength(inventory.Sections[EItemType.Artefact].CurrentTotalStrength);
        _uIManager.SetInventoryStrength(inventory.Sections[EItemType.Spell].CurrentTotalStrength);
        Debug.LogWarning("Инвентарь загружен");
        OnInventoryLoaded?.Invoke();
        spellEquipmentManager.Initialize();
    }

    private void GetStrengthInventory()
    {
        _uIManager.SetInventoryStrength(inventory.Sections[currentInventorySection].CurrentTotalStrength);
    }
    
    public void SetCurrentInventorySection(EItemType section)
    {
        currentInventorySection = section;
        _uIManager.SetInventoryStrength(inventory.Sections[currentInventorySection].CurrentTotalStrength);
    }

    private void AddWeaponItem1()
    {
        inventory.AddItem(testItemBaseWeapon1);
    }
    
    private void AddWeaponItem2()
    {
        inventory.AddItem(testItemBaseWeapon2);
    }
    
    private void AddArtefactItem()
    {
        inventory.AddItem(testItemBaseArtefact);
    }
    
    private void AddSpellItem()
    {
        inventory.AddItem(testItemBaseSpell);
    }
    
    private void AddArtefactItem1()
    {
        inventory.AddItem(testItemBaseArtefact1);
    }
    
    private void AddSpellItem1()
    {
        inventory.AddItem(testItemBaseSpell1);
    }

    private void SaveInventory()
    {
        _inventorySaveLoader.SetData(inventory);
    }

    public void SetCurrentSelectedItem(int slotId)
    {
        if (inventory.Sections[currentInventorySection].GetSlotById(slotId).ItemBase != null)
        {
            _uIManager.SetItemDescriptionText(itemStorage.GetItemDescriptionById(inventory.Sections[currentInventorySection].GetSlotById(slotId).ItemBase.ItemId).description);
        }
    }
}
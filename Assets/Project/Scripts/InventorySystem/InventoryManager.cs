using System;
using UnityEngine;
using Zenject;

[Serializable]
public class InventoryManager : MonoBehaviour, IInitialize<InventoryManager>, IActivate<InventoryManager>
{
    private Controller _controller;
    
    public Inventory Inventory { get; private set; }
    public EItemType CurrentInventorySection { get; private set; } = EItemType.Weapon;
    
    [Inject, SerializeField] private InventoryUIManager uIManager;
    
    [SerializeField] private InventoryPreset inventoryPreset;
    [SerializeField] private InventoryIconManager inventoryIconManager;
    [SerializeField] private ItemStorage itemStorage;
    [SerializeField] private bool initializeByDefault;
    
    [field: SerializeField] public bool IsCheckpoint { get; set; }
    public event Action OnInventoryLoaded;
    
    public void Activate()
    {
        _controller.Enable();
        
        uIManager.OnChangeInventorySection += SetCurrentInventorySection;
        
        Inventory.Sections[EItemType.Artefact].OnChangedStrengthInventory += GetStrengthInventory;
        Inventory.Sections[EItemType.Spell].OnChangedStrengthInventory += GetStrengthInventory;
        Inventory.Sections[EItemType.Spell].OnItemMoved += ManageEquippedItem;
        Inventory.Sections[EItemType.Weapon].OnItemMoved += ManageEquippedItem;
        Inventory.Sections[EItemType.Artefact].OnItemMoved += ManageEquippedItem;
        uIManager.VisibleAllObjects(false);
    }

    public void Deactivate()
    {
        _controller.Disable();
        
        uIManager.OnChangeInventorySection -= SetCurrentInventorySection;
        Inventory.Sections[EItemType.Artefact].OnChangedStrengthInventory -= GetStrengthInventory;
        Inventory.Sections[EItemType.Spell].OnChangedStrengthInventory -= GetStrengthInventory;
    }
    
    public void Initialize()
    {
        inventoryIconManager.Initialize();
        inventoryPreset.Initialize();
        _controller = new Controller();
        
        if (SaveLoadManager.Instance.GetGameData<Inventory>("Inventory") == null)
        {
            Inventory = inventoryPreset.GetDefaultInventoryPreset();
            Inventory.ItemStorage = itemStorage;
        }
        else
        {
            Inventory = SaveLoadManager.Instance.GetGameData<Inventory>("Inventory");
            Inventory.ItemStorage = itemStorage;
        }
        
        foreach (EItemType type in Enum.GetValues(typeof(EItemType)))
        {
            var section = Inventory.Sections[type].GetAllNotEmptyInventorySlots();
            
            for (int i = 0; i < section.Count; i++)
            {
                var slot = Inventory.Sections[type].GetAllNotEmptyInventorySlots()[i];
                var item = itemStorage.GetItemById(slot.ItemId);
                
                if (Inventory.Sections[type].GetEquippedSlotByItemId(slot.ItemId) != null)
                {
                    inventoryIconManager.SetIcon(new SlotTransferInfo(
                        slot.SlotId,
                        Inventory.Sections[type].GetEquippedSlotByItemId(slot.ItemId).SlotId,
                        type,
                        Inventory.Sections[type].GetEquippedSlotByItemId(slot.ItemId).IsEquipment
                        ), item.IsLocked, item.Icon);
                    continue;
                }

                inventoryIconManager.SetIcon(type, slot.SlotId, item.IsLocked, item.Icon);
            }
        }
        uIManager.SetInventoryStrength(Inventory.Sections[EItemType.Artefact].CurrentStrength);
        uIManager.SetInventoryStrength(Inventory.Sections[EItemType.Spell].CurrentStrength);
        Debug.LogWarning("Инвентарь загружен");
        OnInventoryLoaded?.Invoke();
    }

    private void ManageEquippedItem(SlotTransferInfo slotTransferInfo)
    {
        var slot = Inventory.Sections[CurrentInventorySection].GetSlotBySlotId(slotTransferInfo.StandardSlotId);
        var item = itemStorage.GetItemById(slot.ItemId);
        inventoryIconManager.SetIcon(slotTransferInfo, item.IsLocked, item.Icon);
    }

    private void GetStrengthInventory()
    {
        uIManager.SetInventoryStrength(Inventory.Sections[CurrentInventorySection].CurrentStrength);
    }
    
    public void SetCurrentInventorySection(EItemType section)
    {
        CurrentInventorySection = section;
        uIManager.SetInventoryStrength(Inventory.Sections[CurrentInventorySection].CurrentStrength);
    }

    public void SetCurrentSelectedItem(int slotId)
    {
        var inventorySection = Inventory.Sections[CurrentInventorySection];
        if (inventorySection.GetItemIdBySlotId(slotId) != null)
        {
            var item = itemStorage.GetItemById(inventorySection.GetItemIdBySlotId(slotId));
            if (!item.IsLocked)
            {
                float itemStrength = (item as SpellItemBase)?.Strength ?? (item as ArtefactItemBase)?.Strength ?? 0;
                uIManager.SetItemDescriptionText(
                    item.ItemName,
                    item.ItemDescription, 
                    item.Icon, 
                    itemStrength, 
                    CurrentInventorySection);
            }
        }
    }
}
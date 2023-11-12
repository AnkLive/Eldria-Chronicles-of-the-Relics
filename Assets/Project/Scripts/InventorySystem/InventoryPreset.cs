using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New inventory preset", menuName = "Inventory/Inventory preset", order = 0)]
public class InventoryPreset : ScriptableObject
{
    [SerializeField] private List<ItemBase> weaponItemList = new();
    [SerializeField] private List<ItemBase> spellItemList = new();
    [SerializeField] private List<ItemBase> artefactItemList = new();
    
    [SerializeField] private float startTotalStrengthLimitSpellSection;
    [SerializeField] private float startTotalStrengthLimitArtefactSection;
    
    [SerializeField] private int countWeaponEquipmentSlots;
    [SerializeField] private int countSpellEquipmentSlots;
    [SerializeField] private int countArtefactEquipmentSlots;
    
    private readonly Dictionary<EItemType, List<InventorySlot>> _inventorySectionsList = new();

    private bool _isInitialized;

    public void Initialize()
    {
        _inventorySectionsList.Add(EItemType.Spell, InitInventorySlots(spellItemList, countSpellEquipmentSlots));
        _inventorySectionsList.Add(EItemType.Weapon, InitInventorySlots(weaponItemList, countWeaponEquipmentSlots));
        _inventorySectionsList.Add(EItemType.Artefact, InitInventorySlots(artefactItemList, countArtefactEquipmentSlots));
        _isInitialized = true;
    }

    public float GetTotalStrengthSection(EItemType sectionType)
    {
        return sectionType switch
        {
            EItemType.Spell => startTotalStrengthLimitSpellSection,
            EItemType.Artefact => startTotalStrengthLimitArtefactSection,
            _ => 0
        };
    }
    
    public Inventory GetDefaultInventoryPreset()
    {
        if (!_isInitialized)
        {
            Initialize();
            _isInitialized = true;
        }

        Dictionary<EItemType, InventorySection> sections = new();
        
        foreach (EItemType type in Enum.GetValues(typeof(EItemType)))
        {
            sections.Add(type, new InventorySection(
                type,
                GetTotalStrengthSection(type),
                0,
                _inventorySectionsList[type]
                ));
        }
        return new Inventory(sections);
    }
    
    private List<InventorySlot> InitInventorySlots(List<ItemBase> itemBases, int countEquipmentSlots)
    {
        List<InventorySlot> inventorySlots = new();
        
        for (int i = 0; i < itemBases.Count; i++)
        {
            inventorySlots.Add(new InventorySlot(i, itemBases[i].ItemId, false, false));
        }

        for (int i = 0; i < countEquipmentSlots; i++)
        {
            inventorySlots.Add(new InventorySlot(itemBases.Count + i, null, true, true));
        }

        return inventorySlots;
    }
}
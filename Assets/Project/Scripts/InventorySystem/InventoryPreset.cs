using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New inventory preset", menuName = "Inventory/Inventory preset", order = 0)]
public class InventoryPreset : ScriptableObject
{
    [SerializeField] private List<InventorySlot> weaponItemList = new();
    [SerializeField] private List<InventorySlot> spellItemList = new();
    [SerializeField] private List<InventorySlot> artefactItemList = new();
    
    [SerializeField] private float startTotalStrengthLimitSpellSection;
    [SerializeField] private float startTotalStrengthLimitArtefactSection;
    
    private readonly Dictionary<EItemType, List<InventorySlot>> _inventorySectionsList = new();

    private bool _isInitialized;

    public void Initialize()
    {
        _inventorySectionsList.Add(EItemType.Spell, spellItemList);
        _inventorySectionsList.Add(EItemType.Weapon, weaponItemList);
        _inventorySectionsList.Add(EItemType.Artefact, artefactItemList);
        _isInitialized = true;
    }

    private float GetTotalStrengthSection(EItemType sectionType)
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
}
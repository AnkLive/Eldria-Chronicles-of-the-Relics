using UnityEngine;

public class PlayerAttributesManager : MonoBehaviour
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private ItemStorage _itemStorage;
    [SerializeField] private PlayerAttributes _playerAttributes;

    public void UpdateStats(SlotTransferInfo slotTransferInfo)
    {
        ItemBase item = _itemStorage.GetItemById(_inventoryManager.Inventory
            .Sections[slotTransferInfo.InventoryType].GetItemIdBySlotId(slotTransferInfo.StandardSlotId));
        
        switch (slotTransferInfo.InventoryType)
        {
            case EItemType.Weapon:
                    UpdateWeaponStats(item);
                return;
            case EItemType.Spell:
                if (slotTransferInfo.IsEquipment)
                {
                    AddSpellStats(item);
                    return;
                }
                RemoveSpellStats(item);
                return;
            case EItemType.Artefact:
                if (slotTransferInfo.IsEquipment)
                {
                    AddArtefactStats(item);
                    return;
                }
                RemoveArtefactStats(item);
                return;
        }
    }

    private void UpdateWeaponStats(ItemBase item)
    {
        if (item is WeaponItemBase weaponItemBase)
        {
            _playerAttributes.StatusType = weaponItemBase.StatusType;
            _playerAttributes.AttackDamage = weaponItemBase.AttackDamage;
            _playerAttributes.AttackSpeed = weaponItemBase.AttackSpeed;
            _playerAttributes.FireDamageMultiplier = weaponItemBase.FireDamageMultiplier;
            _playerAttributes.IceDamageMultiplier = weaponItemBase.IceDamageMultiplier;
            _playerAttributes.PoisonDamageMultiplier = weaponItemBase.PoisonDamageMultiplier;
            _playerAttributes.AttackDamageMultiplier = weaponItemBase.AttackDamageMultiplier;
            _playerAttributes.CriticalChance = weaponItemBase.CriticalChance;
            _playerAttributes.ElementalChance = weaponItemBase.ElementalChance;
        }
    }
    
    private void AddSpellStats(ItemBase item)
    {
        
    }
    
    private void AddArtefactStats(ItemBase item)
    {
        
    }
    
    private void RemoveSpellStats(ItemBase item)
    {
        
    }
    
    private void RemoveArtefactStats(ItemBase item)
    {
        
    }
}
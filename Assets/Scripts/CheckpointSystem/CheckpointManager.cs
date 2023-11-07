using UnityEngine;
using Zenject;

public class CheckpointManager : MonoBehaviour
{
    [Inject] private ISaveLoader<PlayerAttributes> _playerAttributes;
    [Inject] private ISaveLoader<Inventory> _inventory;
    [SerializeField] private InventoryManager inventoryManager;
    public bool IsCheckpoint { get; private set; }

    private void Start()
    {
        inventoryManager.inventory.Sections[EItemType.Spell].OnEquippedItem += UpdateStats;
        inventoryManager.inventory.Sections[EItemType.Weapon].OnEquippedItem += UpdateStats;
        inventoryManager.inventory.Sections[EItemType.Artefact].OnEquippedItem += UpdateStats;
    }

    public void UpdateData(PlayerUnit playerUnit, int checkpointId, ELevels level)
    {
        IsCheckpoint = false;
        inventoryManager.isCheckpoint = IsCheckpoint;
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.currentCheckpoint = checkpointId;
        playerUnit.MovementComponent.SetFields(_playerAttributes.GetData());
        playerUnit.HealthComponent.SetFields(_playerAttributes.GetData());
        playerUnit.DamageComponent.SetFields(_playerAttributes.GetData());
        SaveData();
        Debug.LogWarning("[CheckpointManager] - Покинут чекпоинт");
    }

    public void SaveData()
    {
        IsCheckpoint = true;
        inventoryManager.isCheckpoint = IsCheckpoint;
        _playerAttributes.SetData(_playerAttributes.GetData());
        Debug.LogWarning("[CheckpointManager] - данные персонажа успешно сохранены");
        _inventory.SetData(inventoryManager.inventory);
        Debug.LogWarning("[CheckpointManager] - данные инвентаря успешно сохранены");
    }

    private void UpdateStats(ItemBase itemBase, bool isEquipment)
    {
        Debug.LogWarning(itemBase == null);
        if (itemBase != null)
        {
            PlayerAttributes attributes = _playerAttributes.GetData();
            if (itemBase is WeaponItemBase weaponItemBase)
            {
                if (isEquipment)
                {
                    attributes.StatusType = weaponItemBase.StatusType;
                    attributes.AttackDamage -= weaponItemBase.AttackDamage;
                    attributes.FireDamageMultiplier += weaponItemBase.FireDamageMultiplier;
                    attributes.IceDamageMultiplier += weaponItemBase.IceDamageMultiplier;
                    attributes.PoisonDamageMultiplier += weaponItemBase.PoisonDamageMultiplier;
                    attributes.AttackDamageMultiplier += weaponItemBase.AttackDamageMultiplier;
                    attributes.CriticalChance += weaponItemBase.CriticalChance;
                    attributes.ElementalChance += weaponItemBase.ElementalChance;
                    attributes.AttackSpeed += weaponItemBase.AttackSpeed;
                    return;
                }
                else
                {
                    attributes.StatusType = EStatusType.Default;
                    attributes.AttackDamage -= weaponItemBase.AttackDamage;
                    attributes.FireDamageMultiplier -= weaponItemBase.FireDamageMultiplier;
                    attributes.IceDamageMultiplier -= weaponItemBase.IceDamageMultiplier;
                    attributes.PoisonDamageMultiplier -= weaponItemBase.PoisonDamageMultiplier;
                    attributes.AttackDamageMultiplier -= weaponItemBase.AttackDamageMultiplier;
                    attributes.CriticalChance -= weaponItemBase.CriticalChance;
                    attributes.ElementalChance -= weaponItemBase.ElementalChance;
                    attributes.AttackSpeed -= weaponItemBase.AttackSpeed;
                    return;
                }
            }
            _playerAttributes.SetData(attributes);
        }
        Debug.LogWarning("Ошибка");
    }
}
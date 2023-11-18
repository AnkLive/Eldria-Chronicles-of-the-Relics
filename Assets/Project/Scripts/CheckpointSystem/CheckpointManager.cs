using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlayerAttributesManager playerAttributesManager;
    public bool IsCheckpoint { get; private set; }

    private void Start()
    {
        inventoryManager.Inventory.Sections[EItemType.Spell].OnItemMoved += playerAttributesManager.UpdateStats;
        inventoryManager.Inventory.Sections[EItemType.Weapon].OnItemMoved += playerAttributesManager.UpdateStats;
        inventoryManager.Inventory.Sections[EItemType.Artefact].OnItemMoved += playerAttributesManager.UpdateStats;
    }
    
    public void UpdateData(PlayerUnit playerUnit, int checkpointId, ELevels level)
    {
        IsCheckpoint = false;
        inventoryManager.IsCheckpoint = IsCheckpoint;
        GameManager.Instance.currentLevel = level;
        GameManager.Instance.currentCheckpoint = checkpointId;
        playerUnit.MovementComponent.UpdateMovementComponentData(UpdateMovementComponentStats(playerUnit.PlayerAttributes));
        playerUnit.DamageComponent.UpdateDamageComponentData(UpdateDamageComponentStats(playerUnit.PlayerAttributes));
        playerUnit.HealthComponent.UpdateHealthComponentData(UpdateHealthComponentStats(playerUnit.PlayerAttributes));
        SaveLoadManager.Instance.SetGameData("PlayerAttributes", playerUnit.PlayerAttributes);
        Debug.LogWarning("[CheckpointManager] - данные персонажа успешно сохранены");
        SaveLoadManager.Instance.SetGameData("Inventory", inventoryManager.Inventory);
        Debug.LogWarning("[CheckpointManager] - данные инвентаря успешно сохранены");
        SaveData();
        Debug.LogWarning("[CheckpointManager] - Покинут чекпоинт");
    }

    public void SaveData()
    {
        IsCheckpoint = true;
        inventoryManager.IsCheckpoint = IsCheckpoint;
        SaveLoadManager.Instance.SaveGame();
    }
    
    private HealthComponentData UpdateHealthComponentStats(PlayerAttributes attributes)
    {
        return new HealthComponentData
        {
            IsImmortal = attributes.IsImmortal,
            HasShieldAbility = attributes.HasShieldAbility,
            IsImmortalDuringThrowAbility = attributes.IsImmortalDuringThrowAbility,
            MaxHealth = attributes.MaxHealth + attributes.MaxHealthMultiplier,
            Armor = attributes.Armor + attributes.ArmorMultiplier,
            FireResistance = attributes.FireResistance + attributes.FireResistanceMultiplier,
            IceResistance = attributes.IceResistance + attributes.IceResistanceMultiplier,
            PoisonResistance = attributes.PoisonResistance + attributes.PoisonResistanceMultiplier
        };
    }
    
    private DamageComponentData UpdateDamageComponentStats(PlayerAttributes attributes)
    {
        return new DamageComponentData
        {
            StatusType = attributes.StatusType,
            AttackDamage = attributes.AttackDamage,
            AttackSpeed = attributes.AttackSpeed + attributes.AttackSpeedMultiplier,
            FireDamageMultiplier = attributes.FireDamageMultiplier,
            IceDamageMultiplier = attributes.IceDamageMultiplier,
            PoisonDamageMultiplier = attributes.PoisonDamageMultiplier,
            AttackDamageMultiplier = attributes.AttackDamageMultiplier,
            CriticalChance = attributes.CriticalChance,
            ElementalChance = attributes.ElementalChance
        };
    }
    
    private MovementComponentData UpdateMovementComponentStats(PlayerAttributes attributes)
    {
        return new MovementComponentData
        {
            CanMove = attributes.CanMove,
            CanJump = attributes.CanJump,
            CanDashing = attributes.CanDash,
            MovementSpeed = attributes.MovementSpeed + attributes.MovementSpeedMultiplier,
            AirborneMovementSpeed = attributes.AirborneMovementSpeed,
            FallingSpeed = attributes.FallingSpeed,
            MaxJumpHeight = attributes.MaxJumpHeight,
            JumpForce = attributes.JumpForce,
            MaxFallSpeed = attributes.MaxFallSpeed,
            UpwardForce = attributes.UpwardForce,
            DashingCooldown = attributes.DashingCooldown + attributes.DashingCooldownMultiplier,
            DashingPower = attributes.DashingPower,
            DashingTime = attributes.DashingTime,
            GroundCheckDistance = attributes.GroundCheckDistance,
            GroundMask = attributes.GroundMask
        };
    }
}
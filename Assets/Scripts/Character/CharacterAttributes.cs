using UnityEngine;

public abstract class CharacterAttributes : ScriptableObject
{
    [field: Header("")]
    [field: SerializeField] public bool IsImmortal { get; set; }
    [field: SerializeField] public bool CanShield { get; set; }
    [field: SerializeField] public bool CanJump { get; set; }
    [field: SerializeField] public bool CanFly { get; set; }
    [field: SerializeField] public bool CanDash { get; set; }
    [field: SerializeField] public bool CanAttack { get; set; }
    [field: SerializeField] public bool CanUseSpell { get; set; }
    [field: SerializeField] public bool CanMove { get; set; }
    [field: Header("")]
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float CurrentHealth { get; set; }
    [field: SerializeField] public float MaxShield { get; set; }
    [field: SerializeField] public float CurrentShield { get; set; }
    [field: Header("")]
    [field: SerializeField] public float MovementSpeed { get; set; }
    [field: SerializeField] public float AirborneMovementSpeed { get; set; }
    [field: Header("")]
    [field: SerializeField] public float AttackDamage { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public float AttackStatusChance { get; set; }
}
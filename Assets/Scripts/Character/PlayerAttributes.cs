using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Player")]
[Serializable]
public class PlayerAttributes : CharacterAttributes
{
    [field: Header("")]
    [field: SerializeField] public float MaxMana { get; set; }
    [field: SerializeField] public float CurrentManaAmount { get; set; }
    [field: Header("")]
    [field: SerializeField] public float MaxManaMultiplier { get; set; }
    [field: Header("")]
    [field: SerializeField] public float SpellRangeMultiplier { get; set; }
    [field: SerializeField] public float SpellDamageMultiplier { get; set; }
    [field: SerializeField] public float SpellRecoveryTimeMultiplier { get; set; }
    [field: SerializeField] public float SpellStatusChanceMultiplier { get; set; }
    [field: Header("")]
    [field: SerializeField] public float MaxJumpHeight { get; set; }
    [field: SerializeField] public float JumpForce { get; set; }
    [field: SerializeField] public float UpwardForce { get; set; }
    [field: SerializeField] public float FlySpeed { get; set; }
    [field: SerializeField] public float FallingSpeed { get; set; }
    [field: SerializeField] public float MaxFallSpeed { get; set; }
    [field: Header("")]
    [field: SerializeField] public float DashingCooldown { get; set; }
    [field: SerializeField] public float DashingPower { get; set; }
    [field: SerializeField] public float DashingTime { get; set; }
    [field: Header("")]
    [field: SerializeField] public float DashingCooldownMultiplier { get; set; }
    [field: Header("")]
    [field: SerializeField] public float MaxHealthMultiplier { get; set; }
    [field: SerializeField] public float MaxShieldMultiplier { get; set; }
    [field: Header("")]
    [field: SerializeField] public float MovementSpeedMultiplier { get; set; }
    [field: Header("")]
    [field: SerializeField] public float AttackDamageMultiplier { get; set; }
    [field: SerializeField] public float AttackSpeedMultiplier { get; set; }
    [field: SerializeField] public float AttackRangeMultiplier { get; set; }
    [field: SerializeField] public float AttackStatusChanceMultiplier { get; set; }
}
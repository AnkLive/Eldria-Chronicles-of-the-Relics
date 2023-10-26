﻿using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;

public enum StatusType
{
    Default,
    Fire,
    Ice,
    Poison
}

public abstract class CharacterAttributes : ScriptableObject
{
    #region Const
    
    [JsonIgnore] private const string CharacterSettings = "Character Settings";
    [JsonIgnore] private const string GeneralSettings = "General Settings";
    [JsonIgnore] private const string HealthSettings = "Health Settings";
    [JsonIgnore] private const string MovementSettings = "Movement Settings";
    [JsonIgnore] private const string AttackSettings = "Attack Settings";

    #endregion
    
    #region General Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(CharacterSettings), Space]
    [field: SerializeField, Foldout(GeneralSettings)] public bool IsImmortal { get; set; }
    [field: SerializeField, Foldout(GeneralSettings)] public bool HasShieldAbility { get; set; }
    [field: SerializeField, Foldout(GeneralSettings)] public bool CanMove { get; set; }
    [field: SerializeField, Foldout(GeneralSettings)] public bool CanJump { get; set; }
    [field: SerializeField, Foldout(GeneralSettings)] public bool CanFly { get; set; }
    [field: SerializeField, Foldout(GeneralSettings)] public bool CanDash { get; set; }
    [field: SerializeField, Foldout(GeneralSettings)] public bool CanAttack { get; set; }
    [field: SerializeField, Foldout(GeneralSettings)] public bool CanUseSpell { get; set; }

    #endregion
    
    #region Health Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(CharacterSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float MaxHealth { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float CurrentHealth { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float Armor { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float FireResistance { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float IceResistance { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float PoisonResistance { get; set; }
    
    #endregion
    
    #region Movement Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(CharacterSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(MovementSettings)] public float MovementSpeed { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(MovementSettings)] public float AirborneMovementSpeed { get; set; }
    
    #endregion
    
    #region Attack Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(CharacterSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float AttackDamage { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float FireDamageMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float IceDamageMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float PoisonDamageMultiplier { get; set; }
    [field: SerializeField, Foldout(AttackSettings)] public StatusType StatusType { get; set; }
    
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float AttackSpeed { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float AttackRange { get; set; }
    
    #endregion
}
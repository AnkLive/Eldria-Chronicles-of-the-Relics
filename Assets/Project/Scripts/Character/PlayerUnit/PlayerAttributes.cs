using System;
using NaughtyAttributes;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Player")]
[Serializable]
public class PlayerAttributes : CharacterAttributes
{
    #region Const
    
    [JsonIgnore] private const string AdditionalSettings = "Player Additional Settings";
    [JsonIgnore] private const string PlayerSettings = "Player Settings";
    [JsonIgnore] private const string ManaSettings = "Mana Settings";
    [JsonIgnore] private const string JumpSettings = "Jump Settings";
    [JsonIgnore] private const string DashSettings = "Dash Settings";
    [JsonIgnore] private const string GroundCheckSettings = "Ground check Settings";
    [JsonIgnore] private const string AttackSettings = "Attack Settings";
    [JsonIgnore] private const string MovementSettings = "Movement Settings";
    [JsonIgnore] private const string HealthSettings = "Health Settings";
    [JsonIgnore] private const string SpellsSettings = "Spells Settings";
    [JsonIgnore] private const string AbilitySettings = "Ability Settings";
    
    #endregion
    
    #region Mana Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(PlayerSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(ManaSettings)] public float MaxMana { get; set; }
    
    #endregion
    
    #region Jump Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(PlayerSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(JumpSettings)] public float MaxJumpHeight { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(JumpSettings)] public float JumpForce { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(JumpSettings)] public float UpwardForce { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(JumpSettings)] public float FallingSpeed { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(JumpSettings)] public float MaxFallSpeed { get; set; }
    
    #endregion
    
    #region Dash Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(PlayerSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(DashSettings)] public float DashingCooldown { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(DashSettings)] public float DashingPower { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(DashSettings)] public float DashingTime { get; set; }
    
    #endregion
    
    #region Ground Check Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(PlayerSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(GroundCheckSettings)] public float GroundCheckDistance { get; set; }
    [field: SerializeField, Foldout(GroundCheckSettings), Layer] public int GroundMask { get; set; }
    
    #endregion
    
    #region Attack Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(AdditionalSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float AttackDamageMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float CriticalChance { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float ElementalChance { get; set; }
    
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float AttackSpeedMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(AttackSettings)] public float AttackRangeMultiplier { get; set; } //!!!
    
    #endregion
    
    #region Movement Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(AdditionalSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(MovementSettings)] public float MovementSpeedMultiplier { get; set; }
    
    #endregion
    
    #region Additional Health Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(AdditionalSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float MaxHealthMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float ArmorMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float FireResistanceMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float IceResistanceMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(HealthSettings)] public float PoisonResistanceMultiplier { get; set; }
    
    #endregion
    
    #region Additional Dash Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(AdditionalSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(DashSettings)] public float DashingCooldownMultiplier { get; set; }
    
    #endregion
    
    #region Additional Mana Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(AdditionalSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(ManaSettings)] public float MaxManaMultiplier { get; set; }
    
    #endregion
    
    #region Additional Spells Settings
    
    [field: HorizontalLine(color: EColor.Gray)]
    [field: Header(AdditionalSettings), Space]
    [field: SerializeField, Range(0, 100), Foldout(SpellsSettings)] public float SpellRangeMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(SpellsSettings)] public float SpellDamageMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(SpellsSettings)] public float SpellRecoveryTimeMultiplier { get; set; }
    [field: SerializeField, Range(0, 100), Foldout(SpellsSettings)] public float SpellStatusChanceMultiplier { get; set; }
    
    #endregion
    
    #region Ability

    [field: SerializeField, Foldout(AbilitySettings)] public bool HasShieldAbility { get; set; }
    [field: SerializeField, Foldout(AbilitySettings)] public bool IsImmortalDuringThrowAbility { get; set; }
    
    #endregion
}
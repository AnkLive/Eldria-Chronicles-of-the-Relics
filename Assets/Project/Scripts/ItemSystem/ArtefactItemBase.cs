using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Artefact Item", menuName = "Inventory/Items/Artefact Item", order = 0)]
public class ArtefactItemBase : ItemBase
{
    public ArtefactItemBase() : base(EItemType.Artefact) { }

    [field: SerializeField] public bool СanDealBodyDamageAbility { get; set; }
    [field: SerializeField] public bool HasShieldAbility { get; set; }
    [field: SerializeField] public bool IsImmortalDuringThrowAbility { get; set; }
    [field: SerializeField, Range(0, 100)] public float Strength { get; set; }
    [field: SerializeField, Range(0, 100)] public float DashingCooldownMultiplier { get; set; }
    [field: SerializeField, Range(0, 100)] public float MaxHealthMultiplier { get; set; }
    [field: SerializeField, Range(0, 100)] public float MovementSpeedMultiplier { get; set; }
    [field: SerializeField, Range(0, 100)] public float AttackSpeedMultiplier { get; set; }
    [field: SerializeField, Range(0, 100)] public float AttackRangeMultiplier { get; set; }
    [field: SerializeField, Range(0, 100)] public float CriticalChance { get; set; }
    [field: SerializeField, Range(0, 100)] public float ElementalChance { get; set; }
    [field: SerializeField, Range(0, 100)] public float AttackDamageMultiplier { get; set; }
    [field: SerializeField, Range(0, 100)] public float ArmorMultiplier { get; set; }
}
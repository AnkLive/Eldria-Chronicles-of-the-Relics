using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Items/Weapon Item", order = 0)]
public class WeaponItemBase : ItemBase
{
    public WeaponItemBase() : base(EItemType.Weapon) { }

    [field: SerializeField] public EStatusType StatusType                      { get; set; }
    [field: SerializeField, Range(0, 100)] public float AttackDamage           { get; set; }
    [field: SerializeField, Range(0, 100)] public float FireDamageMultiplier   { get; set; }
    [field: SerializeField, Range(0, 100)] public float IceDamageMultiplier    { get; set; }
    [field: SerializeField, Range(0, 100)] public float PoisonDamageMultiplier { get; set; }
    [field: SerializeField, Range(0, 100)] public float AttackDamageMultiplier { get; set; }
    [field: SerializeField, Range(0, 100)] public float CriticalChance         { get; set; }
    [field: SerializeField, Range(0, 100)] public float ElementalChance        { get; set; }
    [field: SerializeField, Range(0, 100)] public float AttackSpeed            { get; set; }
}
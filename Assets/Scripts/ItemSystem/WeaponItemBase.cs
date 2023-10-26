using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Items/Weapon Item", order = 0)]
public class WeaponItemBase : ItemBase
{
    public WeaponItemBase() : base(EItemType.Weapon) { }

    [field: SerializeField] public float Damage { get; set; }
}
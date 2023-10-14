using System;
using UnityEngine;

namespace ItemSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Weapon Item", menuName = "Inventory/Items/Weapon Item", order = 0)]
    public class WeaponItem : Item
    {
        public WeaponItem() : base(EItemType.Weapon) { }

        [field: SerializeField] public float Damage { get; set; }
    }
}
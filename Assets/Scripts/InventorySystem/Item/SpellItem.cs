using System;
using UnityEngine;

namespace ItemSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Spell Item", menuName = "Inventory/Items/Spell Item", order = 0)]
    public class SpellItem : Item
    {
        public SpellItem() : base(EItemType.Spell) { }

        [field: SerializeField] public float Strength { get; set; }
        [field: SerializeField] public float Damage { get; set; }
        [field: SerializeField] public float Cooldown { get; set; }
    }
}
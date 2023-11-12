using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Artefact Item", menuName = "Inventory/Items/Artefact Item", order = 0)]
public class ArtefactItemBase : ItemBase
{
    public ArtefactItemBase() : base(EItemType.Artefact) { }

    [field: SerializeField] public float Strength { get; set; }
    [field: SerializeField] public float BoostDamage { get; set; }
    [field: SerializeField] public float BoostHealth { get; set; }
    [field: SerializeField] public float BoostMovementSpeed { get; set; }
}
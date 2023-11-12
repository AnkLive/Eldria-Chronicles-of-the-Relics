using NaughtyAttributes;
using UnityEngine;

public enum EItemType
{
    Spell,
    Weapon,
    Artefact
}

public abstract class ItemInfoBase : ScriptableObject
{
    [field: SerializeField] public string ItemId      { get; set; }
    [field: SerializeField] public EItemType ItemType { get; set; }
    [field: SerializeField] public string ItemName { get; set; }
    [field: SerializeField, ResizableTextArea] public string ItemDescription { get; set; }
    [field: SerializeField, ShowAssetPreview] public Sprite Icon { get; set; }
    [field: SerializeField] public bool IsLocked { get; set; }
    [field: SerializeField] public bool IsEquipment { get; set; }

}

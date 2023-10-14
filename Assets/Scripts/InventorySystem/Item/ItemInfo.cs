using Newtonsoft.Json;
using UnityEngine;

public enum EItemType
{
    Spell,
    Weapon,
    Artefact
}

namespace ItemSystem
{
    public abstract class ItemInfo : ScriptableObject
    {
        [field: SerializeField] public string ItemId      { get; set; }
        [field: SerializeField] public EItemType ItemType { get; set; }
        
        public bool IsEquipment { get; set; }
        
        [JsonIgnore] public string ItemDescription { get; set; }
        [JsonIgnore] public Sprite Icon { get; set; }
    }
}

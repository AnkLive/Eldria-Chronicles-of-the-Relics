using System;
using UnityEngine;

namespace ItemSystem
{
    [CreateAssetMenu(menuName = "Items/Item")]
    [Serializable]
    public class Item : ItemInfo
    {
        public Item() { }
        protected Item(EItemType itemType)
        {
            ItemType = itemType;
        }
    }
}
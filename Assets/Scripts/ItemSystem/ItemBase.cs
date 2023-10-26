using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
[Serializable]
public class ItemBase : ItemInfoBase
{
    public ItemBase() { }
    protected ItemBase(EItemType itemType)
    {
        ItemType = itemType;
    }
}
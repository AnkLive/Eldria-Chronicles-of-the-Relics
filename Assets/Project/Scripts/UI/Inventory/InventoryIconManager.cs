using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryIconManager : MonoBehaviour
{
    [field: SerializeField] public List<UIPanelContainer> UIPanels { get; set; } = new();
    [SerializeField] private Sprite lockedItemIcon;
    [SerializeField] private Sprite emptyItemIcon;

    public void Initialize()
    {
        foreach (var panel in UIPanels)
        {
            panel.Initialize();
        }
    }

    public void SetIcon(EItemType type, int slotId, bool hasLocked, Sprite sprite)
    {
        if (hasLocked)
        {
            sprite = lockedItemIcon;
        }
        
        foreach (var panel in UIPanels)
        {
            var slotsInfo = panel.slotInfoList;
            
            if (!panel.isInitialized)
            {
                panel.Initialize();
            }
            
            if (panel.panelType == type)
            {
                GetUIItemIconSetter(panel.panelUIItemIconSetterList, slotsInfo, slotId).SetIcon(sprite);
            }
        }
    }
    
    public void SetIcon(SlotTransferInfo slotTransferInfo, bool hasLocked, Sprite sprite)
    {
        if (hasLocked)
        {
            sprite = lockedItemIcon;
        }
        
        foreach (var panel in UIPanels)
        {
            var slotsInfo = panel.slotInfoList;
            UIItemIconSetter iconSetter;
            
            if (!panel.isInitialized)
            {
                panel.Initialize();
            }
            
            if (panel.panelType == slotTransferInfo.InventoryType)
            {
                iconSetter = GetUIItemIconSetter(panel.panelUIItemIconSetterList, slotsInfo, slotTransferInfo.StandardSlotId);
                iconSetter.SetIcon(sprite, slotTransferInfo.IsEquipment);
                
                if (!slotTransferInfo.IsEquipment)
                {
                    sprite = emptyItemIcon;
                }
                iconSetter = GetUIItemIconSetter(panel.panelUIItemIconSetterList, slotsInfo, slotTransferInfo.EquipSlotId);
                iconSetter.SetIcon(sprite);
            }
        }
    }

    private UIItemIconSetter GetUIItemIconSetter(List<UIItemIconSetter> listIcons, List<SlotInfo> slotsInfo, int id)
    {
        foreach (var slots in slotsInfo)
        {
            
            if (slots.GetSlotId() == id)
            {
                return listIcons[slots.GetSlotId()];
            }
        }
        
        return null;
    }
}

[Serializable]
public class UIPanelContainer
{
    public void Initialize()
    {
        SetSlotInfoList(inventoryPanel);
        SetSlotInfoList(equipPanel);
        SetUIItemIconSetterList(inventoryPanel);
        SetUIItemIconSetterList(equipPanel);
        isInitialized = true;
    }
    
    public EItemType panelType;
    public Transform equipPanel;
    public Transform inventoryPanel;

    public bool isInitialized;

    private int _slotId;

    [HideInInspector] public List<UIItemIconSetter> panelUIItemIconSetterList;
    [HideInInspector] public List<SlotInfo> slotInfoList;

    private void SetSlotInfoList(Transform panel)
    {
        for (int i = 0; i < panel.childCount; i++)
        {
            slotInfoList.Add(panel.GetChild(i).gameObject.GetComponent<SlotInfo>());
            panel.GetChild(i).gameObject.GetComponent<SlotInfo>().SetSlotId(_slotId);
            _slotId++;
        }
    }

    private void SetUIItemIconSetterList(Transform panel)
    {
        for (int i = 0; i < panel.childCount; i++)
        {
            panelUIItemIconSetterList.Add(panel.GetChild(i).gameObject.GetComponent<UIItemIconSetter>());
        }
    }
}
using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

public interface IInventorySaveLoader
{
    public InventorySaveLoader.InventorySectionDataContainer GetData(EItemType itemType);
    public void SetData(Inventory inventory);
}

public class InventorySaveLoader : MonoBehaviour, IInitialize<InventorySaveLoader>, IInventorySaveLoader
{
    private string _path;
    private IDataHandler<InventoryDataContainer> _dataHandler;
    
    public void Initialize()
    {
        _path = Application.dataPath + "/SaveFile/save_inventory.json";
        _dataHandler = new DataHandler<InventoryDataContainer>(_path);
        LoadData();
        Debug.LogWarning("InventorySaveLoader");
    }

    public InventorySectionDataContainer GetData(EItemType itemType)
    {
        if (_dataHandler.LoadData() != null)
        {
            return _dataHandler.LoadData().InventorySections.TryGetValue(itemType, out var section) ? section : new InventorySectionDataContainer();
        }

        return null;
    }
    
    public void SetData(Inventory inventory)
    {
        var data = new InventoryDataContainer();
        data.InventorySections.Clear();

        for (int i = 0; i < inventory.Sections.Count; i++)
        {
            EItemType itemType = (EItemType)i;
            data.InventorySections[itemType] = new InventorySectionDataContainer
            {
                TotalStrengthLimit = inventory.Sections[itemType].TotalStrengthLimit,
                CurrentTotalStrength = inventory.Sections[itemType].CurrentTotalStrength
            };

            for (int j = 0; j < inventory.Sections[itemType].GetCountSlotInventory(); j++)
            {
                if (inventory.Sections[itemType].GetSlotById(j).Item != null)
                {
                    data.InventorySections[itemType].InventorySection.Add(inventory.Sections[itemType].GetSlotById(j));
                }
            }
        }
        _dataHandler.SaveData(data);
    }

    private void LoadData()
    {
        var data = _dataHandler.LoadData();

        if (data == null)
        {
            data = new InventoryDataContainer();
        }

        if (data.InventorySections == null)
        {
            data.InventorySections = new Dictionary<EItemType, InventorySectionDataContainer>();
        }
        Debug.LogWarning("Данные инвентаря загружены");
    }
    
    [Serializable]
    private class InventoryDataContainer
    {
        public Dictionary<EItemType, InventorySectionDataContainer> InventorySections = new();
    }
    
    [Serializable]
    public class InventorySectionDataContainer
    {
        public List<InventorySlot> InventorySection = new();
        public float TotalStrengthLimit;
        public float CurrentTotalStrength;
    }
}

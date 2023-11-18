// using System.Collections.Generic;
// using System.IO;
// using UnityEngine;
//
// public class InventorySaveLoader : MonoBehaviour, IInitialize<InventorySaveLoader>, ISaveLoader<Inventory>
// {
//     private string _path;
//     private IDataHandler<Inventory> _dataHandler;
//     private Inventory _data;
//     
//     public void Initialize()
//     {
//         _path = Path.Combine(Application.dataPath, "Project/SaveFile/save_inventory.json");
//         _dataHandler = new DataHandler<Inventory>(_path);
//         LoadData();
//     }
//
//     public Inventory GetData()
//     {
//         return _data;
//     }
//     
//     public void SetData(Inventory data)
//     {
//         _data = data;
//         _dataHandler.SaveData(data);
//     }
//
//     private void LoadData()
//     {
//         _data = _dataHandler.LoadData();
//         
//         if (_data == null)
//         {
//             _data = new Inventory(new Dictionary<EItemType, InventorySection>());
//             Debug.LogWarning("Загрузка [InventorySaveLoader]: данные не были загружены или файл данных пуст");
//         }
//         else
//         {
//             Debug.LogWarning("Загрузка [InventorySaveLoader]: загружено");
//         }
//     }
// }

// using System.IO;
// using UnityEngine;
//
// public class PlayerStatsSaveLoader : MonoBehaviour, IInitialize<PlayerStatsSaveLoader>, ISaveLoader<PlayerAttributes>
// {
//     private string _path;
//     private IDataHandler<PlayerAttributes> _dataHandler;
//     private PlayerAttributes _data;
//
//     public void Initialize()
//     {
//         _path = Path.Combine(Application.dataPath, "Project/SaveFile/player_stats.json");
//         _dataHandler = new DataHandler<PlayerAttributes>(_path);
//         LoadData();
//     }
//
//     public PlayerAttributes GetData()
//     {
//         return _data;
//     }
//
//     public void SetData(PlayerAttributes data)
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
//             _data = ScriptableObject.CreateInstance<PlayerAttributes>();
//             Debug.LogWarning("Загрузка [PlayerStatsSaveLoader]: данные не были загружены или файл данных пуст");
//         }
//         else
//         {
//             Debug.LogWarning("Загрузка [PlayerStatsSaveLoader]: загружено");
//         }
//     }
// }
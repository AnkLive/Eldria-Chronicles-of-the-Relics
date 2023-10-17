using System.IO;
using UnityEngine;

public class PlayerStatsSaveLoader : MonoBehaviour, IInitialize<PlayerStatsSaveLoader>, ISaveLoader<Player>
{
    private string _path;
    private IDataHandler<Player> _dataHandler;
    private Player _data;

    public void Initialize()
    {
        _path = Path.Combine(Application.dataPath, "/SaveFile/save_vars.json");
        _dataHandler = new DataHandler<Player>(_path);
        LoadData();
    }

    public Player GetData()
    {
        return _data;
    }

    public void SetData(Player data)
    {
        _data = data;
        _dataHandler.SaveData(data);
    }

    private void LoadData()
    {
        _data = _dataHandler.LoadData();

        if (_data == null)
        {
            _data = ScriptableObject.CreateInstance<Player>();
            Debug.LogWarning("Загрузка [PlayerStatsSaveLoader]: данные не были загружены или файл данных пуст");
        }
        else
        {
            Debug.LogWarning("Загрузка [PlayerStatsSaveLoader]: загружено");
        }
    }
}
using System.IO;
using UnityEngine;

public class InputVarsSaveLoader : MonoBehaviour, IInitialize<InputVarsSaveLoader>, ISaveLoader<GlobalStringVars>
{
    private string _path;
    private IDataHandler<GlobalStringVars> _dataHandler;
    private GlobalStringVars _data;

    public void Initialize()
    {
        _path = Path.Combine(Application.dataPath, "/SaveFile/save_vars.json");
        _dataHandler = new DataHandler<GlobalStringVars>(_path);
        LoadData();
    }

    public void SetData(GlobalStringVars data)
    {
        _data = data;
        _dataHandler.SaveData(data);
    }

    public GlobalStringVars GetData()
    {
        return _data;
    }
    
    private void LoadData()
    {
        _data = _dataHandler.LoadData();
        
        if (_data == null)
        {
            _data = ScriptableObject.CreateInstance<GlobalStringVars>();
            Debug.LogWarning("Загрузка [GlobalStringVars]: данные не были загружены или файл данных пуст");
        }
        else
        {
            Debug.LogWarning("Загрузка [GlobalStringVars]: загружено");
        }
    }
}
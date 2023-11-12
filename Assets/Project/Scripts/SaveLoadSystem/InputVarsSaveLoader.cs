using System.IO;
using UnityEngine;

public class InputVarsSaveLoader : MonoBehaviour, IInitialize<InputVarsSaveLoader>, ISaveLoader<StringVariableManager>
{
    private string _path;
    private IDataHandler<StringVariableManager> _dataHandler;
    private StringVariableManager _data;

    public void Initialize()
    {
        _path = Path.Combine(Application.dataPath, "Project/SaveFile/save_vars.json");
        _dataHandler = new DataHandler<StringVariableManager>(_path);
        LoadData();
    }

    public void SetData(StringVariableManager data)
    {
        _data = data;
        _dataHandler.SaveData(data);
    }

    public StringVariableManager GetData()
    {
        return _data;
    }
    
    private void LoadData()
    {
        _data = _dataHandler.LoadData();
        
        if (_data == null)
        {
            _data = ScriptableObject.CreateInstance<StringVariableManager>();
            Debug.LogWarning("Загрузка [GlobalStringVars]: данные не были загружены или файл данных пуст");
        }
        else
        {
            Debug.LogWarning("Загрузка [GlobalStringVars]: загружено");
        }
    }
}
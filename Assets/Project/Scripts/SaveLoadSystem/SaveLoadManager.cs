using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager _instance;
    public static SaveLoadManager Instance => _instance;
    
    private readonly Repository _repository = new();
    private string _path;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        _path = Path.Combine(Application.dataPath, "Project/SaveFile/game_data.json");
        DontDestroyOnLoad(gameObject);
    }

    [ContextMenu("Load Game")]
    public void LoadGame()
    {
        _repository.LoadData(_path);
    }

    [ContextMenu("Save Game")]
    public void SaveGame()
    {
        _repository.SaveData(_path);
    }
    
    public T GetGameData<T>(string key)
    {
        return _repository.GetData<T>(key);
    }

    public void SetGameData<T>(string key, T data)
    {
        _repository.SetData<T>(key, data);
    }
}
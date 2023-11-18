using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class Repository
{
    private Dictionary<string, object> _gameState = new();

    public void SaveData(string path)
    {
        try
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(_gameState, Formatting.Indented));
            Debug.LogWarning($"Сохранение [Repository] - Успешно: произошло сохранение");
        }
        catch (JsonException e)
        {
            Debug.LogError($"Сохранение [Repository] - Ошибка: ошибка сохранения данных:\n{e.Message}");
        }
    }

    public void LoadData(string path)
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _gameState = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            Debug.LogWarning($"Загрузка [Repository] - Успешно: произошла загрузка");
        }
        else
        {
            Debug.LogWarning($"Загрузка [Repository] - Файл сохранения не найден");
        }
    }

    public T GetData<T>(string key)
    {
        if (_gameState.TryGetValue(key, out var value))
        {
            string jsonData = JsonConvert.SerializeObject(value);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        return default; // Можно вернуть null или другое значение по умолчанию
    }
    
    public void SetData<T>(string key, T data)
    {
        _gameState[key] = data;

        try
        {
            Debug.LogWarning($"Установка данных [Repository] - Успешно: произошла запись данных");
        }
        catch (JsonException e)
        {
            Debug.LogError($"Установка данных [Repository] - Ошибка: ошибка записи данных:\n{e.Message}");
        }
    }
}


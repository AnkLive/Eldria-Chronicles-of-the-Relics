using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Formatting = Newtonsoft.Json.Formatting;

// Обобщенный интерфейс для сохранения и загрузки данных
public interface IDataHandler<T>
{
    void SaveData(T data);
    T LoadData();
}

// Класс, отвечающий за сохранение и загрузку данных
public class DataHandler<T> : IDataHandler<T>
{
    private string _path;

    public DataHandler(string savePath)
    {
        _path = savePath;
    }

    public void SaveData(T data)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        try
        {
            File.WriteAllText(_path, json);
            Debug.LogWarning($"[DataHandler] - Успешно: произошло сохранение");
        }
        catch (JsonException e)
        {
            Debug.LogError($"[DataHandler] - Ошибка: ошибка сохранения данных - {data.GetType().Name}:\n{e.Message}");
        }
    }

    public T LoadData()
    {
        if (!File.Exists(_path))
        {
            SaveData(default(T)); // Создание пустых данных по умолчанию
        }

        string json = File.ReadAllText(_path);

        if (string.IsNullOrEmpty(json))
        {
            return default(T); // Возврат пустых данных по умолчанию
        }
        else
        {
            try
            {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.None,
                    NullValueHandling = NullValueHandling.Ignore,
                    Converters = new List<JsonConverter> { new ItemConverter() }
                };
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonException e)
            {
                Debug.LogError($"[DataHandler] - Ошибка: ошибка загрузки данных:\n{e.Message}");
                return default(T); // Возврат пустых данных по умолчанию
            }
        }
    }
}


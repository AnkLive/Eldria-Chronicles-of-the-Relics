using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [Inject] private IInitialize<InventoryManager> _inventoryManagerInitialize;
    [Inject] private IInitialize<InventoryUIManager> _inventoryUIManagerInitialize;
    
    [Inject] private IActivate<InventoryManager> _inventoryManagerActivate;
    [Inject] private IActivate<InventoryUIManager> _inventoryUIManagerActivate;
    
    private bool _isLoading;
    
    public LoadingLevel level;
    public ELevels levelName;
    
    public event Action UIInitialized;
    public event Action DataLoaded;
    
    private void Start()
    {
        StartLoadGameData();
        StartInitializeObjects();
    }

    private void StartInitializeObjects()
    {
        StartCoroutine(Initialize());
        _isLoading = false;
        level.LoadLevelFromAddressable(levelName, GameManager.Instance.currentCheckpoint);
    }

    private void StartLoadGameData()
    {
        StartCoroutine(LoadGameData());
        _isLoading = false;
    }
    
    private IEnumerator LoadGameData()
    {
        _isLoading = true;
        Debug.LogWarning("Загрузка [LevelManager]: загрузка...");
        yield return new WaitUntil(() => !_isLoading);
        Debug.LogWarning("Загрузка [LevelManager]: данные загружены");
        DataLoaded?.Invoke();
    }
    
    private IEnumerator Initialize()
    {
        _isLoading = true;
        Debug.LogWarning("Загрузка [LevelManager]: инициализация...");
        SaveLoadManager.Instance.LoadGame();
        _inventoryUIManagerInitialize.Initialize();
        _inventoryManagerInitialize.Initialize();
        
        _inventoryUIManagerActivate.Activate();
        _inventoryManagerActivate.Activate();
        yield return new WaitUntil(() => !_isLoading);
        Debug.LogWarning("Загрузка [LevelManager]: объекты инициализированны");
        UIInitialized?.Invoke();
    }
    
    private IEnumerator CloseGamePlayScene()
    {
        _isLoading = true;
        Debug.LogWarning("Выход из сцены [LevelManager]: сохранение данных");
        _inventoryUIManagerActivate.Deactivate();
        _inventoryManagerActivate.Deactivate();
        yield return new WaitUntil(() => !_isLoading);
        Debug.LogWarning("Выход из сцены [LevelManager]: данные сохранены");
        UIInitialized?.Invoke();
    }
}
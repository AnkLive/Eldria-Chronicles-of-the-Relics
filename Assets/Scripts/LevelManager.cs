using System;
using System.Collections;
using InventorySystem;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [Inject]
    private IInitialize<PlayerStatsSaveLoader> _playerStatsInitialize;
    [Inject]
    private IInitialize<InventorySaveLoader> _inventoryInitialize;
    [Inject]
    private IInitialize<InputVarsSaveLoader> _inputVarsInitialize;
    [Inject]
    private IInitialize<InventoryManager> _inventoryManager;
    
    private bool _isLoading;
    
    public LoadingLevel level;
    public string levelName;
    
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
        level.LoadLevelFromAddressable(levelName);
    }

    private void StartLoadGameData()
    {
        StartCoroutine(LoadGameData());
        _isLoading = false;
    }
    
    private IEnumerator LoadGameData()
    {
        _isLoading = true;
        Debug.LogWarning("Загрузка [LoadGameData]: загрузка...");
        _inventoryInitialize.Initialize();
        _playerStatsInitialize.Initialize();
        _inputVarsInitialize.Initialize();
        yield return new WaitUntil(() => !_isLoading);
        Debug.LogWarning("Загрузка [LoadGameData]: данные загружены");
        DataLoaded?.Invoke();
    }
    
    private IEnumerator Initialize()
    {
        _isLoading = true;
        Debug.LogWarning("Загрузка [Initialize]: инициализация...");
        _inventoryManager.Initialize();
        yield return new WaitUntil(() => !_isLoading);
        Debug.LogWarning("Загрузка [Initialize]: объекты инициализированны");
        UIInitialized?.Invoke();
    }
}
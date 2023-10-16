using System;
using System.Collections;
using InventorySystem;
using UnityEngine;
using Zenject;

public interface IInitialize<T>
{
    public void Initialize();
}

public class GameLoadingManager : MonoBehaviour
{
    [Inject]
    private ISaveLoader<Player> playerStatsSaveLoader;
    [Inject]
    private IInitialize<PlayerStatsSaveLoader> playerStatsInitialize;
    [Inject]
    private IInitialize<InventorySaveLoader> inventorySaveLoader;
    [Inject]
    private IInitialize<InputVarsSaveLoader> inputVarsSaveLoader;
    public  InventoryManager InventoryManager { get; set; }
    
    private bool _isLoading;
    
    public event Action DataLoaded;
    public event Action UIInitialized;

    public void StartLoadGameData()
    {
        StartCoroutine(LoadGameData());
        _isLoading = false;
    }

    public void StartInitializeObjects()
    {
        StartCoroutine(InitializeUI());
        _isLoading = false;
    }

    private IEnumerator LoadGameData()
    {
        _isLoading = true;
        Debug.Log("Loading data...");
        inventorySaveLoader.Initialize();
        playerStatsInitialize.Initialize();
        inputVarsSaveLoader.Initialize();
        yield return new WaitUntil(() => !_isLoading);
        Debug.Log("Data loaded.");
        DataLoaded?.Invoke();
    }

    private IEnumerator InitializeUI()
    {
        _isLoading = true;
        Debug.Log("Initializing...");
        InventoryManager.Initialize();
        yield return new WaitUntil(() => !_isLoading);
        Debug.Log("UI initialized.");
        UIInitialized?.Invoke();
    }
}
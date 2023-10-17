using System;
using System.Collections;
using InventorySystem;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [Inject]
    private IInitialize<PlayerStatsSaveLoader> playerStatsInitialize;
    [Inject]
    private IInitialize<InventorySaveLoader> inventoryInitialize;
    [Inject]
    private IInitialize<InputVarsSaveLoader> inputVarsInitialize;
    [Inject]
    private ISaveLoader<Player> playerStatsSaveLoader;
    [Inject]
    public IPlayerStatsModifier playerStatsModifier;
    [Inject]
    private IInitialize<InventoryManager> InventoryManager;
    
    private bool _isLoading;
    
    public event Action UIInitialized;
    public event Action DataLoaded;
    
    private void Start()
    {
        StartLoadGameData();
        playerStatsModifier.SetPlayer(playerStatsSaveLoader.GetData());
        StartInitializeObjects();
    }
    
    public void StartInitializeObjects()
    {
        StartCoroutine(Initialize());
        _isLoading = false;
    }
    
    public void StartLoadGameData()
    {
        StartCoroutine(LoadGameData());
        _isLoading = false;
    }
    
    private IEnumerator LoadGameData()
    {
        _isLoading = true;
        Debug.Log("Loading data...");
        inventoryInitialize.Initialize();
        playerStatsInitialize.Initialize();
        inputVarsInitialize.Initialize();
        yield return new WaitUntil(() => !_isLoading);
        Debug.Log("Data loaded.");
        DataLoaded?.Invoke();
    }
    
    private IEnumerator Initialize()
    {
        _isLoading = true;
        Debug.Log("Initializing...");
        InventoryManager.Initialize();
        yield return new WaitUntil(() => !_isLoading);
        Debug.Log("UI initialized.");
        UIInitialized?.Invoke();
    }
}
using System;
using System.Collections;
using InventorySystem;
using UnityEngine;

public class GameLoadingManager : MonoBehaviour
{
    public  InventoryManager InventoryManager { get; set; }
    
    [SerializeField] private InventorySaveLoader inventorySaveLoader;
    [SerializeField] private PlayerStatsSaveLoader playerStatsSaveLoader;
    [SerializeField] private InputVarsSaveLoader inputVarsSaveLoader;
    
    private bool _isLoading;

    public event Action DataLoaded;
    public static event Action UIInitialized;

    private void LoadData()
    {
        inventorySaveLoader.Initialize();
        playerStatsSaveLoader.Initialize();
        inputVarsSaveLoader.Initialize();
    }

    private void InitializeObjects()
    {
        InventoryManager.inventorySaveLoader = inventorySaveLoader;
        InventoryManager.Initialize();
    }

    public void StartLoadGameData()
    {
        StartCoroutine(LoadGameData());
    }

    public void StartInitializeObjects()
    {
        StartCoroutine(InitializeUI());
    }

    private IEnumerator LoadGameData()
    {
        _isLoading = true;
        Debug.Log("Loading data...");
        LoadData();
        yield return new WaitUntil(() => !_isLoading);
        Debug.Log("Data loaded.");
        DataLoaded?.Invoke();
    }

    private IEnumerator InitializeUI()
    {
        Debug.Log("Initializing UI...");
        InitializeObjects();
        yield return new WaitUntil(() => !_isLoading);
        Debug.Log("UI initialized.");
        UIInitialized?.Invoke();
    }
}
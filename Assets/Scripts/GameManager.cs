using InventorySystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    public LoadingLevel level;
    public string levelName;
    public GameLoadingManager gameLoadingManager;
    public static GameManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoadingGameScene")
        {
            gameLoadingManager.StartLoadGameData();
            SceneManager.LoadScene("MainMenuScene");
        }
        else if (scene.name == "GamePlayScene")
        {
            gameLoadingManager.InventoryManager = FindObjectOfType<InventoryManager>();
            gameLoadingManager.StartInitializeObjects();
            
            level.LoadLevelFromAddressable(levelName);
        }
    }
}
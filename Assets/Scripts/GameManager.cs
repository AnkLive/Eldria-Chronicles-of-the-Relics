using InventorySystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject] private SceneLoader _sceneLoader;
    public LoadingLevel level;
    public string levelName;
    public GameLoadingManager gameLoadingManager;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sceneLoader.LoadScene("GamePlayScene");
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
            _sceneLoader.LoadScene("MainMenuScene");
        }
        else if (scene.name == "GamePlayScene")
        {
            level.LoadLevelFromAddressable(levelName);
        }
    }
}
using UnityEngine;

public enum ELevels
{
    TestLevel
}

public class GameManager : MonoBehaviour
{
    public ELevels currentLevel;
    public int currentCheckpoint;
    public static GameManager Instance { get; private set; }
    
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
}
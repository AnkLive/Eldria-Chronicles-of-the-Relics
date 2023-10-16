using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader : MonoBehaviour
{
    [Inject]
    DiContainer container;

    public void LoadScene(string sceneName)
    {
        container.UnbindAll();
        SceneManager.LoadScene(sceneName);
    }
}
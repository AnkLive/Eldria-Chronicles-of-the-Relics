using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingLevel : MonoBehaviour
{
    public PlayerSpawner player;
    public async void LoadLevelFromAddressable(string levelName)
    {
        // Загрузить уровень из Addressable Assets
        AsyncOperationHandle<GameObject> levelHandle = Addressables.LoadAssetAsync<GameObject>(levelName);
        await levelHandle.Task;

        if (levelHandle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject levelPrefab = levelHandle.Result;

            // Создать экземпляр уровня на сцене
            GameObject levelInstance = Instantiate(levelPrefab);
            
            Transform spawnPointTransform = levelInstance.transform.Find("SpawnPoint");
            await player.Spawn(spawnPointTransform);
        }
        else
        {
            Debug.LogError("Failed to load level: " + levelName);
        }

        
    }
}

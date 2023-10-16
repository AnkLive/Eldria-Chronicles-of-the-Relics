using Platformer.MovementSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class LoadingLevel : MonoBehaviour
{
    [Inject]
    private DiContainer container;
    public async void LoadLevelFromAddressable(string levelName)
    {
        // Загрузить уровень из Addressable Assets
        AsyncOperationHandle<GameObject> levelHandle = Addressables.LoadAssetAsync<GameObject>(levelName);
        await levelHandle.Task;
        levelHandle.Completed += async (operation) =>
        {
            if (levelHandle.IsValid())
            {
                Addressables.Release(levelHandle);
            }
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject levelPrefab = operation.Result;

                // Создать экземпляр уровня на сцене
                GameObject levelInstance = Instantiate(levelPrefab);

                // Найдем и получим ссылку на объект spawnPoint
                Transform spawnPoint = levelInstance.transform.Find("SpawnPoint");

                if (spawnPoint != null)
                {
                    // Загрузите префаб игрока из Addressable Assets
                    AsyncOperationHandle<GameObject> playerHandle = Addressables.LoadAssetAsync<GameObject>("Player");
                    await playerHandle.Task;

                    if (playerHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        GameObject playerPrefab = playerHandle.Result;

                        // Создайте экземпляр игрока и установите его позицию в spawnPoint
                        GameObject playerInstance = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

                        // Получите компонент Movement из игрока и выполните инициализацию
                        Movement playerMovement = playerInstance.GetComponent<Movement>();
                        container.Inject(playerMovement);
                        if (playerMovement != null)
                        {
                            playerMovement.Initialize();
                        }
                    }
                    else
                    {
                        Debug.LogError("Failed to load player prefab");
                    }
                }
                else
                {
                    Debug.LogError("spawnPoint not found in the level");
                }
            }
            else
            {
                Debug.LogError("Failed to load level: " + levelName);
            }
        };
    }
}

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingLevel : MonoBehaviour
{
    public CheckpointManager _CheckpointManager;
    public PlayerSpawner player;
    public async void LoadLevelFromAddressable(ELevels levelName, int checkpointId)
    {
        // Загрузить уровень из Addressable Assets
        AsyncOperationHandle<GameObject> levelHandle = Addressables.LoadAssetAsync<GameObject>(levelName.ToString());
        await levelHandle.Task;

        if (levelHandle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject levelPrefab = levelHandle.Result;

            // Создать экземпляр уровня на сцене
            GameObject levelInstance = Instantiate(levelPrefab);
            CheckpointController[] checkpointControllers = levelInstance.GetComponentsInChildren<CheckpointController>();
            
            for (int i = 0; i < checkpointControllers.Length; i++)
            {
                checkpointControllers[i].CheckpointId = i;
                checkpointControllers[i].LevelId = levelName;
                checkpointControllers[i].CheckpointManager = _CheckpointManager;
                if (i == checkpointId)
                {
                    await player.Spawn(checkpointControllers[i].gameObject.transform);
                }
            }
        }
        else
        {
            Debug.LogError("Failed to load level: " + levelName);
        }

        
    }
}

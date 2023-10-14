using Platformer.MovementSystem;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class LoadingLevel : MonoBehaviour
{
    public void LoadLevelFromAddressable(string levelName)
    {
        // Загрузить уровень из Addressable Assets
        AsyncOperationHandle<GameObject> levelHandle = Addressables.LoadAssetAsync<GameObject>(levelName);

        levelHandle.Completed += (operation) =>
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject levelPrefab = operation.Result;
                // Создать экземпляр уровня на сцене
                GameObject levelInstance = Instantiate(levelPrefab);
                // Добавь код для настройки и управления уровнем
                // Например, установка позиции, инициализация компонентов и т.д.
            }
            else
            {
                Debug.LogError("Failed to load level: " + levelName);
            }
        };
    }
}

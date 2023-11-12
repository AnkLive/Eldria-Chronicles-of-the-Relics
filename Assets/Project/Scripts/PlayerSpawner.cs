using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [Inject] private DiContainer _container;
    public async Task Spawn(Transform obj)
    {
        AsyncOperationHandle<GameObject> playerHandle = Addressables.LoadAssetAsync<GameObject>("Player");
        await playerHandle.Task;

        if (playerHandle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject playerPrefab = playerHandle.Result;
            
            GameObject playerInstance = Instantiate(playerPrefab, obj.transform.position, Quaternion.identity);
            PlayerUnit playerUnitMovementComponent = playerInstance.GetComponent<PlayerUnit>();
            if (playerUnitMovementComponent != null)
            {
                _container.InjectGameObject(playerInstance);
            }
        }
    }
}
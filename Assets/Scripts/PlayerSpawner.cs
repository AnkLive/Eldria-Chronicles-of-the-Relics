using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    [Inject]
    private DiContainer container;
    
    public async Task Spawn(Transform obj)
    {
        AsyncOperationHandle<GameObject> playerHandle = Addressables.LoadAssetAsync<GameObject>("Player");
        await playerHandle.Task;
        //container.Inject(playerHandle.Result);

        if (playerHandle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject playerPrefab = playerHandle.Result;
            
            GameObject playerInstance = Instantiate(playerPrefab, obj.transform.position, Quaternion.identity);
            Debug.LogError(container == null);
            CharacterMovement characterMovement = playerInstance.GetComponent<CharacterMovement>();
            if (characterMovement != null)
            {
                Debug.LogError("!!!!!!!!");
                container.InjectGameObject(playerInstance);
                Debug.LogError(container == null);
                characterMovement.Initialize();
            }
        }
    }
}
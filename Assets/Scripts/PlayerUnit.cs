using UnityEngine;
using Zenject;

public class PlayerUnit : MonoBehaviour
{
    private PlayerUnitMovementComponent _movementComponent;
    [Inject] private ISaveLoader<PlayerAttributes> _dataAttributes;
    [SerializeField] private PlayerAttributes playerAttributes;
    
    private void Start()
    {
        playerAttributes = _dataAttributes.GetData();
        _movementComponent = gameObject.GetComponent<PlayerUnitMovementComponent>();
        _movementComponent.SetFields(playerAttributes);
        _movementComponent.Initialize();
        _movementComponent.Activate();
    }
}
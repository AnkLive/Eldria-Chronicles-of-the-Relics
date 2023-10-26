using UnityEngine;
using Zenject;

public class PlayerUnit : MonoBehaviour
{
    [Inject] private IInitialize<PlayerUnitMovementComponent> _playerUnitMovementComponentInitialize;
    [Inject] private IActivate<PlayerUnitMovementComponent> _playerUnitMovementComponentActivate;
    [Inject] private IInitialize<HealthComponent> _healthComponentActivate;
    
    [Inject] private PlayerUnitMovementComponent _playerUnitMovementComponent;
    [Inject] private HealthComponent _healthComponent;
    [Inject] private DamageComponent _damageComponent;
    
    private void Start()
    {
        _playerUnitMovementComponentInitialize.Initialize();
        _playerUnitMovementComponentActivate.Activate();
    }
}
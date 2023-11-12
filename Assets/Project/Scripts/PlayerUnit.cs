using System;
using ModestTree.Util;
using UnityEngine;
using Zenject;

public class PlayerUnit : MonoBehaviour
{
    [Inject] private IInitialize<PlayerUnitMovementComponent> _playerUnitMovementComponentInitialize;
    [Inject] private IActivate<PlayerUnitMovementComponent> _playerUnitMovementComponentActivate;
    [Inject] private ISaveLoader<PlayerAttributes> _playerAttributes;
    
    [field: SerializeField] public HealthComponent HealthComponent { get; private set; }
    [field: SerializeField] public DamageComponent DamageComponent { get; private set; }
    
    [SerializeField] private CollisionHandler collisionHandler;
    
    public IMovable MovementComponent { get; private set; }
    
    private PlayerInput _playerInput;

    public void OnDisable()
    {
        _playerInput.OnAttack -= collisionHandler.LaunchAnAttack;
        _playerInput.OnMove -= ChangeDirectionAttack;
    }
    
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        MovementComponent = GetComponent<MovementComponent>();
        MovementComponent.SetObjectRigidbody(GetComponent<Rigidbody2D>());
        MovementComponent.SetFields(_playerAttributes.GetData());
        HealthComponent.Initialize();
        HealthComponent.SetFields(_playerAttributes.GetData());
        DamageComponent.SetFields(_playerAttributes.GetData());
        _playerUnitMovementComponentInitialize.Initialize();
        _playerUnitMovementComponentActivate.Activate();
        MovementComponent.OnDashing += HealthComponent.SetImmortalDuringThrow;
        _playerInput.OnAttack += collisionHandler.LaunchAnAttack;
        _playerInput.OnMove += ChangeDirectionAttack;
    }

    private void ChangeDirectionAttack(float direction)
    {
        if (direction != 0)
        {
            collisionHandler.gameObject.transform.localPosition =
                new Vector2(direction, collisionHandler.gameObject.transform.localPosition.y);
        }
    }
}
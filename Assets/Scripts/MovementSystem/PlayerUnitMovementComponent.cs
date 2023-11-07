using ModestTree.Util;
using UnityEngine;

public class PlayerUnitMovementComponent : MonoBehaviour, 
    IInitialize<PlayerUnitMovementComponent>, 
    IActivate<PlayerUnitMovementComponent>
{
    #region Fields
    
    private PlayerInput _playerInput;
    private IMovable _movementComponent;
    
    #endregion
    
    #region Methods
    
    #region Main
    
    public void Activate()
    {
        _playerInput.OnMove += Move;
        _playerInput.OnJump += Jump;
        _playerInput.OnDash += Dash;
    }

    public void Deactivate()
    {
        _playerInput.OnMove -= Move;
        _playerInput.OnJump -= Jump;
        _playerInput.OnDash -= Dash;
    }

    public void Initialize()
    {
        _playerInput = GetComponent<PlayerInput>();
        _movementComponent = GetComponent<MovementComponent>();
    }
    
    #endregion

    #region Movement

    private void Move(float direction)
    {
        _movementComponent.CheckGroundedStatus();
        _movementComponent.PerformMove(direction);
    }
    
    #endregion

    #region Jump

    private void Jump(bool isJump)
    {
        _movementComponent.CheckGroundedStatus();
        _movementComponent.PerformJump(isJump);
    }
    
    #endregion

    #region Dash

    private void Dash()
    {
        _movementComponent.CheckGroundedStatus();
        _movementComponent.PerformDash();
    }
    
    #endregion
    
    #endregion
}

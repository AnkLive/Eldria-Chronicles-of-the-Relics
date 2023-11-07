using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Fields
    
    private Controller _controller;
    
    public event Action<float> OnMove;
    public event Action<bool> OnJump;
    public event Action OnDash;
    public event Action OnAttack;
    
    #endregion
    
    #region Methods
    
    #region Main
    
    private void Awake()
    {
        _controller = new Controller();
    }

    private void OnEnable()
    {
        _controller.Main.Dash.performed += _ => Dash();
        _controller.Main.Move.started += _ => MoveStarted();
        _controller.Main.Move.canceled += _ => MoveCanceled();
        _controller.Main.Jump.started += _ => JumpStarted();
        _controller.Main.Jump.canceled += _ => JumpCanceled();
        _controller.Main.LeftMouse.performed += _ => Attack();
        _controller.Enable();
    }

    private void OnDisable()
    {
        _controller.Disable();
    }
    
    #endregion
    
    #region Actions

    private void MoveStarted()
    {
        OnMove?.Invoke(_controller.Main.Move.ReadValue<float>());
    }

    private void MoveCanceled()
    {
        OnMove?.Invoke(0f);
    }
    
    private void JumpStarted()
    {
        OnJump?.Invoke(true);
    }
    
    private void JumpCanceled()
    {
        OnJump?.Invoke(false);
    }
    
    private void Dash()
    {
        OnDash?.Invoke();
    }
    
    private void Attack()
    {
        OnAttack?.Invoke();
    }
    
    #endregion
    
    #endregion
}
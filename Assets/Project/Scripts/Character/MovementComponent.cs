using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

public struct MovementComponentData
{
    public bool CanMove;
    public bool CanJump;
    public bool CanDashing;
    public float MovementSpeed;
    public float AirborneMovementSpeed;
    public float FallingSpeed;
    public float MaxJumpHeight;
    public float JumpForce;
    public float MaxFallSpeed;
    public float UpwardForce;
    public float DashingCooldown;
    public float DashingPower;
    public float DashingTime;
    public float GroundCheckDistance;
    public LayerMask GroundMask;
}

[RequireComponent(typeof(Rigidbody2D))]
public class MovementComponent : MonoBehaviour, IMovable
{
    #region Const
    
    private const string GeneralSettings = "General Settings";
    private const string MovementSettings = "Movement Settings";
    private const string JumpSettings = "Jump Settings";
    private const string DashSettings = "Dash Settings";
    private const string GroundCheckSettings = "Ground check Settings";
    
    #endregion
    
    #region Fields
    
    private Rigidbody2D _objectRigidbody;
    private float _objectMovement;
    private bool _isJump;
    private float _lastPositionBeforeJump;
    private float _currentPositionJump;
    private bool _isMaxHeightJump;
    private float _fallingTime;
    private bool _isGrounded;
    private bool _isDashing;
    private bool _canDash = true;
    private float _originalGravity;
    private float _velocityY;
    private float _modifiedFallingSpeed;
    public event Action<bool> OnDashing;

    private MovementComponentData _movementComponentData;
        
    #endregion

    #region Methods
    
    #region Movement
    
    public void PerformMove(float direction = 0)
    {
        if(_isDashing) return;
        
        _objectRigidbody.velocity = CalculateMovementSpeed(direction);
    }
    
    private Vector2 CalculateMovementSpeed(float direction)
    {
        _objectMovement = direction;
        
        if (_isGrounded)
        {
            return new Vector2(_objectMovement * _movementComponentData.MovementSpeed, CalculateGravityModifier());
        }
        return new Vector2(_objectMovement * _movementComponentData.MovementSpeed / _movementComponentData.AirborneMovementSpeed, CalculateGravityModifier());
    }
    
    #endregion
    
    #region Jump

    public void PerformJump(bool isJump)
    {
        if(_isDashing) return;
        
        UpdateJumpInput(isJump);
        UpdateCurrentPositionJump();

        if (!_isMaxHeightJump)
        {
            _objectRigidbody.velocity = new Vector2(_objectMovement * _movementComponentData.MovementSpeed / _movementComponentData.AirborneMovementSpeed, _movementComponentData.JumpForce);
        }

        UpdateIsMaxHeightJump();
    }

    private void UpdateCurrentPositionJump()
    {
        _currentPositionJump = transform.position.y;
    }

    private void UpdateIsMaxHeightJump()
    {
        _isMaxHeightJump = _currentPositionJump - _lastPositionBeforeJump >= _movementComponentData.MaxJumpHeight;
    }

    private void UpdateLastPositionBeforeJump()
    {
        _isMaxHeightJump = false;
        _lastPositionBeforeJump = transform.position.y;
    }
    
    private void UpdateJumpInput(bool isJump)
    {
        if (isJump && _isGrounded)
        {
            _isJump = true;
        }
        if (!isJump || _isMaxHeightJump)
        {
            _isJump = false;
        }
    }
    
    #endregion
    
    #region Dash

    public void PerformDash()
    {
        if (_canDash)
        {
            StartCoroutine(Dashing());
        }
    }

    private IEnumerator Dashing()
    {
        _canDash = false;
        _isDashing = true;
        OnDashing?.Invoke(true);
        
        _originalGravity = _objectRigidbody.gravityScale;
        _objectRigidbody.gravityScale = 0;
        
        _objectRigidbody.velocity = new Vector2(_objectMovement * _movementComponentData.DashingPower, 0f);

        yield return new WaitForSeconds(_movementComponentData.DashingTime);

        _objectRigidbody.gravityScale = _originalGravity;
        
        _isDashing = false;
        _isJump = false;
        OnDashing?.Invoke(false);

        yield return new WaitForSeconds(_movementComponentData.DashingCooldown);

        _canDash = true;
    }
    
    #endregion
    
    #region Check Ground
    
    private float CalculateGravityModifier()
    {
        _fallingTime += Time.deltaTime;

        if (_isGrounded)
        {
            _fallingTime = 0.0f;
            return 0.0f;
        }

        _velocityY = _objectRigidbody.velocity.y;

        if (_isMaxHeightJump && _velocityY >= 0.0f)
        {
            _fallingTime = 0.0f;
            _velocityY = Mathf.Lerp(_velocityY, 0.0f, Time.deltaTime * _movementComponentData.UpwardForce);
        }
        else if (!_isJump && _velocityY >= 0.0f)
        {
            _velocityY = Mathf.Lerp(_velocityY, 0.0f, Time.deltaTime * _movementComponentData.UpwardForce);
        }
        else if (!_isJump && _velocityY <= 0.0f)
        {
            _modifiedFallingSpeed = -_movementComponentData.FallingSpeed * _fallingTime;
            _modifiedFallingSpeed = Mathf.Clamp(_modifiedFallingSpeed, -_movementComponentData.MaxFallSpeed, 0.0f);
            return _modifiedFallingSpeed;
        }

        return _velocityY;
    }

    public void CheckGroundedStatus()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _movementComponentData.GroundCheckDistance, _movementComponentData.GroundMask);
        
        if (hit.collider != null)
        {
            UpdateLastPositionBeforeJump();
            _isGrounded = true;
            return;
        }
        _isGrounded = false;
    } 

    private void OnDrawGizmos()
    {
        var position = transform.position;
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Vector3 start = position;
        Vector3 end = position + Vector3.down * _movementComponentData.GroundCheckDistance;
        Gizmos.DrawLine(start, end);
    }
    
    #endregion
    
    #region Set Fields

    public void SetObjectRigidbody(Rigidbody2D objectRigidbody)
    {
        _objectRigidbody = objectRigidbody;
    }

    public void UpdateMovementComponentData(MovementComponentData componentData)
    {
        _movementComponentData = componentData;
    }

    #endregion
    
    #endregion
}

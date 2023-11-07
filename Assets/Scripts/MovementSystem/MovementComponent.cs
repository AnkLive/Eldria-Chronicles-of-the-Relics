using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

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
        
    #endregion
    
    #region Inspector Fields
    
    [SerializeField, Foldout(GeneralSettings)] private bool canMove;
    [SerializeField, Foldout(GeneralSettings)] private bool canJump;
    [SerializeField, Foldout(GeneralSettings)] private bool canDashing;
    [SerializeField, Range(0, 100), Foldout(MovementSettings)] private float movementSpeed;
    [SerializeField, Range(1, 100), Foldout(MovementSettings)] private float airborneMovementSpeed;
    [SerializeField, Range(0, 100), Foldout(JumpSettings)] private float fallingSpeed;
    [SerializeField, Range(0, 100), Foldout(JumpSettings)] private float maxJumpHeight;
    [SerializeField, Range(0, 100), Foldout(JumpSettings)] private float jumpForce;
    [SerializeField, Range(0, 100), Foldout(JumpSettings)] private float maxFallSpeed;
    [SerializeField, Range(0, 100), Foldout(JumpSettings)] private float upwardForce;
    [SerializeField, Range(0, 100), Foldout(DashSettings)] private float dashingCooldown;
    [SerializeField, Range(0, 100), Foldout(DashSettings)] private float dashingPower;
    [SerializeField, Range(0, 100), Foldout(DashSettings)] private float dashingTime;
    [SerializeField, Range(0, 100), Foldout(GroundCheckSettings)] private float groundCheckDistance;
    [SerializeField, Foldout(GroundCheckSettings)] private LayerMask groundMask;
    
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
            return new Vector2(_objectMovement * movementSpeed, CalculateGravityModifier());
        }
        return new Vector2(_objectMovement * movementSpeed / airborneMovementSpeed, CalculateGravityModifier());
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
            _objectRigidbody.velocity = new Vector2(_objectMovement * movementSpeed / airborneMovementSpeed, jumpForce);
        }

        UpdateIsMaxHeightJump();
    }

    private void UpdateCurrentPositionJump()
    {
        _currentPositionJump = transform.position.y;
    }

    private void UpdateIsMaxHeightJump()
    {
        _isMaxHeightJump = _currentPositionJump - _lastPositionBeforeJump >= maxJumpHeight;
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
        
        _objectRigidbody.velocity = new Vector2(_objectMovement * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);

        _objectRigidbody.gravityScale = _originalGravity;
        
        _isDashing = false;
        _isJump = false;
        OnDashing?.Invoke(false);

        yield return new WaitForSeconds(dashingCooldown);

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
            _velocityY = Mathf.Lerp(_velocityY, 0.0f, Time.deltaTime * upwardForce);
        }
        else if (!_isJump && _velocityY >= 0.0f)
        {
            _velocityY = Mathf.Lerp(_velocityY, 0.0f, Time.deltaTime * upwardForce);
        }
        else if (!_isJump && _velocityY <= 0.0f)
        {
            _modifiedFallingSpeed = -fallingSpeed * _fallingTime;
            _modifiedFallingSpeed = Mathf.Clamp(_modifiedFallingSpeed, -maxFallSpeed, 0.0f);
            return _modifiedFallingSpeed;
        }

        return _velocityY;
    }

    public void CheckGroundedStatus()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask);
        
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
        Vector3 end = position + Vector3.down * groundCheckDistance;
        Gizmos.DrawLine(start, end);
    }
    
    #endregion
    
    #region Set Fields

    public void SetObjectRigidbody(Rigidbody2D objectRigidbody)
    {
        _objectRigidbody = objectRigidbody;
    }
    
    public void SetFields(PlayerAttributes attributes)
    {
        movementSpeed = attributes.MovementSpeed + attributes.MovementSpeedMultiplier;
        canMove = attributes.CanMove;
        airborneMovementSpeed = attributes.AirborneMovementSpeed;
        canJump = attributes.CanJump;
        fallingSpeed = attributes.FallingSpeed;
        maxJumpHeight = attributes.MaxJumpHeight;
        jumpForce = attributes.JumpForce;
        maxFallSpeed = attributes.MaxFallSpeed;
        upwardForce = attributes.UpwardForce;
        canDashing = attributes.CanDash;
        dashingCooldown = attributes.DashingCooldown + attributes.DashingCooldownMultiplier;
        dashingPower = attributes.DashingPower;
        dashingTime = attributes.DashingTime;
        groundCheckDistance = attributes.GroundCheckDistance;
        groundMask = attributes.GroundMask;
    }
    
    public void GetFields(PlayerAttributes attributes)
    {
        attributes.MovementSpeed = movementSpeed;
        attributes.CanMove = canMove;
        attributes.AirborneMovementSpeed = airborneMovementSpeed;
        attributes.CanJump = canJump;
        attributes.FallingSpeed = fallingSpeed;
        attributes.MaxJumpHeight = maxJumpHeight;
        attributes.JumpForce = jumpForce;
        attributes.MaxFallSpeed = maxFallSpeed;
        attributes.UpwardForce = upwardForce;
        attributes.CanDash = canDashing;
        attributes.DashingCooldown = dashingCooldown;
        attributes.DashingPower = dashingPower;
        attributes.DashingTime = dashingTime;
        attributes.GroundCheckDistance = groundCheckDistance;
        attributes.GroundMask = groundMask;
    }

    #endregion
    
    #endregion
}

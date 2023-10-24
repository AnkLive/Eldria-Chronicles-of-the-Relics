using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
    public class PlayerUnitMovementComponent : MonoCache
    {
        #region Field
        
        private Controller _controller;
        private Rigidbody2D _rigidbodyObject;
        private float _objectMovement;
        private bool _isJump;
        private float _lastPositionBeforeJump;
        private float _currentPositionJump;
        private bool _isMaxHeightJump;
        private float _fallingTime;
        private bool _isGrounded;
        private bool _isDashing;
        private bool _canDash = true;
        private event Action OnGrounded; 
        
        #endregion
        
        #region Inspector Field
        
        [Header("Movement Settings")]
        [SerializeField] private bool canMove;

        [SerializeField, Range(0, 100), ShowIf(nameof(canMove))]
        private float movementSpeed;

        [SerializeField, Range(1, 20), ShowIf(nameof(canMove))]
        private float airborneMovementSpeed;
        
        [Header("Jump Settings")]
        [SerializeField] private bool canJump;

        [SerializeField, Range(0, 100), ShowIf(nameof(canJump))]
        private float fallingSpeed;

        [SerializeField, Range(0, 20), ShowIf(nameof(canJump))]
        private float maxJumpHeight;

        [SerializeField, Range(0, 100), ShowIf(nameof(canJump))]
        private float jumpForce;
        
        [SerializeField, Range(0, 100), ShowIf(nameof(canJump))]
        private float maxFallSpeed;
        
        [SerializeField, Range(0, 100), ShowIf(nameof(canJump))]
        private float upwardForce;
        
        [Header("Dash Settings")]
        [SerializeField] private bool canDashing;
        
        [SerializeField, Range(0, 20), ShowIf(nameof(canDashing))]
        private float dashingCooldown;
        
        [SerializeField, Range(0, 100), ShowIf(nameof(canDashing))]
        private float dashingPower;
        
        [SerializeField, Range(0, 20), ShowIf(nameof(canDashing))]
        private float dashingTime;
        
        [Header("Ground detect Settings")]
        [SerializeField] private float groundCheckDistance;
        
        [SerializeField] private LayerMask groundMask;
        
        #endregion

        #region Methods

        #region Main
        
        public void Activate()
        {
            _controller.Main.Dash.performed += _ => PerformDash();
            OnGrounded += UpdateLastPositionBeforeJump;
            _controller.Enable();
            AddFixedUpdate();
        }

        public void Deactivate()
        {
            OnGrounded -= UpdateLastPositionBeforeJump;
            _controller.Disable();
            RemoveFixedUpdate();
        }

        public void Initialize()
        {
            _controller = new Controller();
            _rigidbodyObject = GetComponent<Rigidbody2D>();
        }

        public override void Run()
        {
            if (_isDashing) return;
            
            SetInputData();
        }

        public override void FixedRun() 
        {
            CheckGroundedStatus();
            
            if (_isDashing) return;

            if (canMove)
            {
                Move();
            }

            if (canJump)
            {
                PerformJump();
            }
        }
        
        private void SetInputData()
        {
            UpdateJumpInput();
            UpdateMovementInput();
        }
        
        #endregion

        #region Movement

        public void Move()
        {
            _rigidbodyObject.velocity = CalculateMovementSpeed();
        }

        private Vector2 CalculateMovementSpeed()
        {
            if (_isGrounded)
            {
                return new Vector2(_objectMovement * movementSpeed, CalculateGravityModifier());
            }
            return new Vector2(_objectMovement * movementSpeed / airborneMovementSpeed, CalculateGravityModifier());
        }
        
        private void UpdateMovementInput()
        {
            _objectMovement = _controller.Main.Move.ReadValue<float>();
        }
        
        #endregion

        #region Jump
        

        public void PerformJump()
        {
            UpdateCurrentPositionJump();

            if (_isJump && !_isMaxHeightJump)
            {
                _rigidbodyObject.velocity = new Vector2(_objectMovement * movementSpeed / airborneMovementSpeed, jumpForce);
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
        
        private void UpdateJumpInput()
        {
            if (_controller.Main.Jump.ReadValue<float>() > 0 && _isGrounded)
            {
                _isJump = true;
            }
            if (_controller.Main.Jump.ReadValue<float>() < 1 || _isMaxHeightJump)
            {
                _isJump = false;
            }
        }
        
        #endregion

        #region Dash

        public void PerformDash()
        {
            if (_canDash)
                StartCoroutine(Dashing());
        }
        
        private IEnumerator Dashing()
        {
            _canDash = false;
            _isDashing = true;
            
            float originalGravity = _rigidbodyObject.gravityScale;
            _rigidbodyObject.gravityScale = 0;
            _rigidbodyObject.velocity = new Vector2(_objectMovement * dashingPower, 0f);

            yield return new WaitForSeconds(dashingTime);

            _rigidbodyObject.gravityScale = originalGravity;
            _isDashing = false;
            _isJump = false;

            yield return new WaitForSeconds(dashingCooldown);

            _canDash = true;
        }
        
        #endregion

        #region Check ground

        private float CalculateGravityModifier()
        {
            _fallingTime += Time.deltaTime;

            if (_isGrounded)
            {
                _fallingTime = 0.0f;
                return 0.0f;
            }

            float velocityY = _rigidbodyObject.velocity.y;

            if (_isMaxHeightJump && velocityY >= 0.0f)
            {
                _fallingTime = 0.0f;
                velocityY = Mathf.Lerp(velocityY, 0.0f, Time.deltaTime * upwardForce);
            }
            else if (!_isJump && velocityY >= 0.0f)
            {
                velocityY = Mathf.Lerp(velocityY, 0.0f, Time.deltaTime * upwardForce);
            }
            else if (!_isJump && velocityY <= 0.0f)
            {
                float modifiedFallingSpeed = -fallingSpeed * _fallingTime;
                modifiedFallingSpeed = Mathf.Clamp(modifiedFallingSpeed, -maxFallSpeed, 0.0f);
                return modifiedFallingSpeed;
            }

            return velocityY;
        }

        public void CheckGroundedStatus()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundMask);
            
            if (hit.collider != null)
            {
                OnGrounded?.Invoke();
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

        #region SetFields

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

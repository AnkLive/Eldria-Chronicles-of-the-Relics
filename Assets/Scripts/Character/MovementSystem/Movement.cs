using System.Collections;
using UnityEngine;
using Zenject;

namespace Platformer.MovementSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : BaseMovement
    {
        private IPlayerStatsModifier playerStatsModifier;

        [Inject]
        public void Construct(IPlayerStatsModifier playerStatsModifier)
        {
            this.playerStatsModifier = playerStatsModifier;
        }
        #region Переменные которые не выводятся в инспекторе

        public Rigidbody2D RigidbodyObject  { get; private set; }
        [Tooltip("Направление движения")]
        public float ObjectMovement         { get; private set; }

        [Tooltip("Совершил ли объект прыжок")]
        public bool  IsJump                 { get; private set; }
        [Tooltip("Последняя позиция перед прыжком")]
        public float LastPositionBeforeJump { get; private set; }
        [Tooltip("Текущая позиция во время прыжка")]
        public float CurrentPositionJump    { get; private set; }
        [Tooltip("Достигнул ли объект максимальной высоты прыжка")]
        public bool IsMaxHeightJump         { get; private set; }
        [Tooltip("Скорость падения")]
        public float FallingTime            { get; private set; }

        [Tooltip("Стоит ли объект на земле")]
        public bool IsGrounded              { get; private set; }

        [Tooltip("Содержит информацию о пользовательском вводе")]
        public InputData Inputs              { get; private set; }
        [Tooltip("пользовательский ввод в виде массива")]
        private IInput[] _inputsArray;

        [Tooltip("Совершил ли объект бросок")]
        public bool IsDashing                { get; private set; }
        [Tooltip("Может ли объект совершить бросок")]
        public bool CanDash                  { get; private set; } = true;

        #endregion

        #region Переменные которые выводятся в инспекторе

        [field:Header("Движение")]
        [field:SerializeField] public bool CanMove                                { get; set; }
        [Tooltip("Кривая ускорения объекта")]
        [field:SerializeField] public AnimationCurve Acceleration                 { get; set; }
        [Tooltip("Мгновенное ускорение")]
        [field:SerializeField] public bool InstantAcceleration                    { get; set; }
        [Tooltip("Постоянное ускорение")]
        [field:SerializeField] public bool ConstantAcceleration                   { get; set; }
        [Tooltip("Скорость движения")]
        [field:SerializeField, Range(0, 100)] public float MovementSpeed          { get; set; }
        [Tooltip("Деление скорости движения когда объект в воздухе")]
        [field:SerializeField, Range(1, 20)] public float AirborneMovementSpeed   { get; set; }

        [field:Header("Настройки прыжка")]
        [field:SerializeField] public bool CanJump                                { get; set; }
        [Tooltip("Скорость падения")]
        [field:SerializeField, Range(0, 100)] public float FallingSpeed           { get; set; }
        [Tooltip("Максимальная высота прыжка")]
        [field:SerializeField, Range(0, 20)] public float MaxJumpHeight           { get; set; }
        [Tooltip("Сила прыжка")]
        [field:SerializeField, Range(0, 100)] public float JumpForce              { get; set; }
        [Tooltip("Максимальная скорость падения")]
        [field:SerializeField, Range(0, 100)] public float MaxFallSpeed           { get; set; }
        [Tooltip("Сила подъема вверх после прыжка")]
        [field:SerializeField, Range(0, 100)] public float UpwardForce            { get; set; }

        [field:Header("Настройки определения земли")]
        [field:SerializeField] public bool CanFly                                 { get; set; }
        [Tooltip("Позиция объекта'Transform'")]
        [field:SerializeField] public Transform GroundColliderTransform           { get; set; }
        [Tooltip("радиус обнаружения земли")]
        [field:SerializeField] public float RaycastRadius                        { get; set; }
        [Tooltip("Слой который будет определяться как земля")]
        [field:SerializeField] public LayerMask GroundMask                        { get; set; }

        [field:Header("Настройки броска")]
        [field:SerializeField] public bool CanDashing                             { get; set; }
        [Tooltip("Скорость восстановления броска")]
        [field:SerializeField, Range(0, 20)] public float DashingCooldown         { get; set; }
        [Tooltip("Сила броска")]
        [field:SerializeField, Range(0, 100)] public float DashingPower           { get; set; }
        [Tooltip("Время броска")]
        [field:SerializeField, Range(0, 20)] public float DashingTime             { get; set; }
        
        #endregion

        #region Методы

        public override void Initialize() 
        {
            RigidbodyObject = GetComponent<Rigidbody2D>();
            _inputsArray = GetComponents<IInput>();
            //MovementSpeed = playerStatsModifier.GetModifiedMovementSpeed();
        }

        private void Update() 
        {
            if(!IsDashing)
            {
                GatherInputs();
                SetInputData();

                if(CanDashing)
                    PerformDash();
            }
        }

        private void FixedUpdate() 
        {
            if(!IsDashing)
            {
                
                if(CanFly)
                    CheckGroundedStatus();

                if(CanMove)
                    Move(CalculateGravityModifier());

                if(CanJump)
                    PerformJump();
            }
        }

        public override void PerformJump()
        {
            UpdateCurrentPositionJump();

            if (IsJump && !IsMaxHeightJump)
                Move(JumpForce);

            UpdateIsMaxHeightJump();

            if (!IsJump)
            {
                UpdateLastPositionBeforeJump();
                IsMaxHeightJump = false;
            }
        }
        
        private void UpdateCurrentPositionJump() 
            => CurrentPositionJump = transform.position.y;

        private void UpdateIsMaxHeightJump() 
            => IsMaxHeightJump = CurrentPositionJump - LastPositionBeforeJump >= MaxJumpHeight;

        private void UpdateLastPositionBeforeJump() 
            => LastPositionBeforeJump = transform.position.y;

        public override void Move(float moveY) 
            => RigidbodyObject.velocity = new Vector2(CalculateAcceleration(), moveY);
        
        public override float CalculateAcceleration() 
            => !IsGrounded 
                ? ObjectMovement * MovementSpeed / AirborneMovementSpeed : InstantAcceleration 
                ? ObjectMovement * MovementSpeed : Acceleration.Evaluate(ObjectMovement) * MovementSpeed;

        public override bool MoveToPosition(Vector2 endPoint)
        {
            RigidbodyObject.MovePosition(Vector2.MoveTowards(RigidbodyObject.position, endPoint, 
                MovementSpeed * Time.deltaTime));
            return RigidbodyObject.position == endPoint;
        }

        public override void PerformDash()
        {
            if (IsDashing && CanDash)
                StartCoroutine(Dashing());
        }

        public override float CalculateGravityModifier()
        {
            FallingTime += Time.deltaTime;

            if (IsGrounded)
            {
                FallingTime = 0.0f;
                return 0.0f;
            }

            float velocityY = RigidbodyObject.velocity.y;

            if (IsMaxHeightJump && velocityY >= 0.0f)
            {
                FallingTime = 0.0f;
                velocityY = Mathf.Lerp(velocityY, 0.0f, Time.deltaTime * UpwardForce);
            }
            else if (!IsJump && velocityY >= 0.0f)
            {
                velocityY = Mathf.Lerp(velocityY, 0.0f, Time.deltaTime * UpwardForce);
            }
            else if (!IsJump && velocityY <= 0.0f)
            {
                float modifiedFallingSpeed = -FallingSpeed * FallingTime;
                modifiedFallingSpeed = Mathf.Clamp(modifiedFallingSpeed, -MaxFallSpeed, 0.0f);
                return modifiedFallingSpeed;
            }

            return velocityY;
        }

        public override void CheckGroundedStatus() 
            => IsGrounded = Physics2D.Raycast(GroundColliderTransform.position, 
                Vector2.down, RaycastRadius, GroundMask).collider != null;

        public override void SetInputData()
        {
            UpdateJumpInput();
            UpdateDashingInput();
            UpdateMovementInput();
        }

        private void UpdateJumpInput()
        {
            if (Inputs.IsJumped && IsGrounded)
                IsJump = true;
            else if (Inputs.Jumped || IsMaxHeightJump)
                IsJump = false;
        }

        private void UpdateDashingInput()
        {
            if (CanDash)
                IsDashing = Inputs.Dash;
        }

        private void UpdateMovementInput()
        {
            ObjectMovement = 0.0f;  // Сбрасываем текущую скорость перед обновлением

            // Проверяем ввод на движение влево и вправо
            if (Inputs.MoveToLeft)
            {
                ObjectMovement = -1.0f; // Устанавливаем скорость влево
            }
            else if (Inputs.MoveToRight)
            {
                ObjectMovement = 1.0f; // Устанавливаем скорость вправо
            }
        }

        public override void GatherInputs()
        {
            Inputs = new InputData();

            foreach (var input in _inputsArray)
            {       
                Inputs = input.GenerateInput();
            }
        }

        private IEnumerator Dashing()
        {
            CanDash = false;
            IsDashing = true;

            float originalGravity = RigidbodyObject.gravityScale;
            RigidbodyObject.gravityScale = 0;
            RigidbodyObject.velocity = new Vector2(ObjectMovement * DashingPower, 0f);

            yield return new WaitForSeconds(DashingTime);

            RigidbodyObject.gravityScale = originalGravity;

            IsDashing = false;
            IsJump = false;

            yield return new WaitForSeconds(DashingCooldown);

            CanDash = true;
        }

        #endregion
    }
}

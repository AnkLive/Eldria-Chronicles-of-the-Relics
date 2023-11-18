using UnityEngine;
using Zenject;

public class PlayerUnit : MonoBehaviour
{
    [Inject] private IInitialize<PlayerUnitMovementComponent> _playerUnitMovementComponentInitialize;
    [Inject] private IActivate<PlayerUnitMovementComponent> _playerUnitMovementComponentActivate;
    
    [field: SerializeField] public HealthComponent HealthComponent { get; private set; }
    [field: SerializeField] public DamageComponent DamageComponent { get; private set; }
    
    [SerializeField] private CollisionHandler collisionHandler;
    [field: SerializeField] public PlayerAttributes PlayerAttributes { get; set; }
    
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
        MovementComponent.UpdateMovementComponentData(new MovementComponentData
        {
            CanMove = PlayerAttributes.CanMove,
            CanJump = PlayerAttributes.CanJump,
            CanDashing = PlayerAttributes.CanDash,
            MovementSpeed = PlayerAttributes.MovementSpeed,
            AirborneMovementSpeed = PlayerAttributes.AirborneMovementSpeed,
            FallingSpeed = PlayerAttributes.FallingSpeed,
            MaxJumpHeight = PlayerAttributes.MaxJumpHeight,
            JumpForce = PlayerAttributes.JumpForce,
            MaxFallSpeed = PlayerAttributes.MaxFallSpeed,
            UpwardForce = PlayerAttributes.UpwardForce,
            DashingCooldown = PlayerAttributes.DashingCooldown,
            DashingPower = PlayerAttributes.DashingPower,
            DashingTime = PlayerAttributes.DashingTime,
            GroundCheckDistance = PlayerAttributes.GroundCheckDistance,
            GroundMask = PlayerAttributes.GroundMask
        });
        DamageComponent.UpdateDamageComponentData(new DamageComponentData
            {
                CanAttack = PlayerAttributes.CanAttack,
                StatusType = PlayerAttributes.StatusType,
                AttackDamage = PlayerAttributes.AttackDamage,
                AttackSpeed = PlayerAttributes.AttackSpeed,
                FireDamageMultiplier = PlayerAttributes.FireDamageMultiplier,
                IceDamageMultiplier = PlayerAttributes.IceDamageMultiplier,
                PoisonDamageMultiplier = PlayerAttributes.PoisonDamageMultiplier,
                AttackDamageMultiplier = PlayerAttributes.AttackDamageMultiplier,
                CriticalChance = PlayerAttributes.CriticalChance,
                ElementalChance = PlayerAttributes.ElementalChance
            });
        HealthComponent.UpdateHealthComponentData(new HealthComponentData
        {
            IsImmortal = PlayerAttributes.IsImmortal,
            HasShieldAbility = PlayerAttributes.HasShieldAbility,
            IsImmortalDuringThrowAbility = PlayerAttributes.IsImmortalDuringThrowAbility,
            MaxHealth = PlayerAttributes.MaxHealth,
            Armor = PlayerAttributes.Armor,
            FireResistance = PlayerAttributes.FireResistance,
            IceResistance = PlayerAttributes.IceResistance,
            PoisonResistance = PlayerAttributes.PoisonResistance
        });
        HealthComponent.Initialize();
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
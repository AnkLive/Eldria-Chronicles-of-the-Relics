using UnityEngine;

public interface IPlayerStatsModifier
{
    void SetPlayer(PlayerAttributes playerAttributes);
    float GetModifiedMovementSpeed();
    bool GetCanMove();
    bool GetCanDash();
    bool GetCanJump();
    float GetAirborneMovementSpeed();
    float GetMaxJumpHeight();
    float GetMaxFallSpeed();
    float GetJumpForce();
    float GetUpwardForce();
    float GetDashingPower();
    float GetDashingTime();
    float GetGroundCheckDistance();
    int GetGroundMask();
    float GetModifiedDashingCooldown();
    void SetModifiedMovementSpeed(float modifiedMovementSpeed);
    void SetCanMove(bool canMove);
    void SetCanDash(bool canDash);
    void SetCanJump(bool canJump);
    void SetAirborneMovementSpeed(float airborneMovementSpeed);
    void SetMaxJumpHeight(float maxJumpHeight);
    void SetMaxFallSpeed(float maxFallSpeed);
    void SetJumpForce(float jumpForce);
    void SetUpwardForce(float upwardForce);
    void SetDashingPower(float dashingPower);
    void SetDashingTime(float dashingTime);
    void SetGroundCheckDistance(float groundCheckDistance);
    void SetGroundMask(int groundMask);
    void SetModifiedDashingCooldown(float modifiedDashingCooldown);
}


public class PlayerStatsModifier : MonoBehaviour, IPlayerStatsModifier
{
    private PlayerAttributes _playerAttributes;
    
    public float GetModifiedMovementSpeed()
    {
        return _playerAttributes.MovementSpeed + _playerAttributes.MovementSpeedMultiplier;
    }
    
    public bool GetCanMove()
    {
        return _playerAttributes.CanMove;
    }
    
    public bool GetCanDash()
    {
        return _playerAttributes.CanDash;
    }
    
    public bool GetCanJump()
    {
        return _playerAttributes.CanJump;
    }
    
    public float GetAirborneMovementSpeed()
    {
        return _playerAttributes.AirborneMovementSpeed;
    }
    
    public float GetMaxJumpHeight()
    {
        return _playerAttributes.MaxJumpHeight;
    }
    
    public float GetMaxFallSpeed()
    {
        return _playerAttributes.MaxFallSpeed;
    }  
    
    public float GetJumpForce()
    {
        return _playerAttributes.JumpForce;
    }  
    
    public float GetUpwardForce()
    {
        return _playerAttributes.UpwardForce;
    }  
    
    public float GetDashingPower()
    {
        return _playerAttributes.DashingPower;
    }  
    
    public float GetDashingTime()
    {
        return _playerAttributes.DashingTime;
    }  
    
    public float GetGroundCheckDistance()
    {
        return _playerAttributes.GroundCheckDistance;
    }  
    
    public int GetGroundMask()
    {
        return _playerAttributes.GroundMask;
    }  
    
    public float GetModifiedDashingCooldown()
    {
        return _playerAttributes.DashingCooldown + _playerAttributes.DashingCooldownMultiplier;
    }

    public void SetPlayer(PlayerAttributes playerAttributes)
    {
        _playerAttributes = playerAttributes;
    }
    
    public void SetModifiedMovementSpeed(float modifiedMovementSpeed)
    {
        _playerAttributes.MovementSpeedMultiplier = modifiedMovementSpeed;
    }  
    
    public void SetCanMove(bool canMove)
    {
        _playerAttributes.CanMove = canMove;
    }
    
    public void SetCanDash(bool canDash)
    {
        _playerAttributes.CanDash = canDash;
    }
    
    public void SetCanJump(bool canJump)
    {
        _playerAttributes.CanJump = canJump;
    }
    
    public void SetAirborneMovementSpeed(float airborneMovementSpeed)
    {
        _playerAttributes.AirborneMovementSpeed = airborneMovementSpeed;
    }
    
    public void SetMaxJumpHeight(float maxJumpHeight)
    {
        _playerAttributes.MaxJumpHeight = maxJumpHeight;
    }
    
    public void SetMaxFallSpeed(float maxFallSpeed)
    {
        _playerAttributes.MaxFallSpeed = maxFallSpeed;
    }  
    
    public void SetJumpForce(float jumpForce)
    {
        _playerAttributes.JumpForce = jumpForce;
    }  
    
    public void SetUpwardForce(float upwardForce)
    {
        _playerAttributes.UpwardForce = upwardForce;
    }  
    
    public void SetDashingPower(float dashingPower)
    {
        _playerAttributes.DashingPower = dashingPower;
    }  
    
    public void SetDashingTime(float dashingTime)
    {
        _playerAttributes.DashingTime = dashingTime;
    }  
    
    public void SetGroundCheckDistance(float groundCheckDistance)
    {
        _playerAttributes.GroundCheckDistance = groundCheckDistance;
    }  
    
    public void SetGroundMask(int groundMask)
    {
        _playerAttributes.GroundMask = groundMask;
    }  
    
    public void SetModifiedDashingCooldown(float modifiedDashingCooldown)
    {
        _playerAttributes.DashingCooldownMultiplier = modifiedDashingCooldown;
    }
}

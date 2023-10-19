using UnityEngine;

public interface IPlayerStatsModifier
{
    void SetPlayer(PlayerAttributes playerAttributes);
    float GetModifiedMovementSpeed();
    // float GetModifiedMaxHealth();
    // float GetModifiedMaxShield();
    // float GetModifiedAttackDamage();
    // float GetModifiedAttackSpeed();
    // float GetModifiedAttackRange();
    // float GetModifiedAttackStatusChance();
    // float GetModifiedMaxMana();
    // float GetModifiedSpellRange(float spellRange);
    // float GetModifiedSpellDamage(float spellDamage);
    // float GetModifiedSpellRecovery(float spellRecovery);
    // float GetModifiedSpellStatusChance(float spellStatusChance);
    float GetModifiedDashingCooldown();
    void SetModifiedMovementSpeed(float modifiedMovementSpeed);
    // void SetModifiedMaxHealth(float modifiedMaxHealth);
    // void SetModifiedMaxShield(float modifiedMaxShield);
    // void SetModifiedAttackDamage(float modifiedAttackDamage);
    // void SetModifiedAttackSpeed(float modifiedAttackSpeed);
    // void SetModifiedAttackRange(float modifiedAttackRange);
    // void SetModifiedAttackStatusChance(float modifiedAttackStatusChance);
    // void SetModifiedMaxMana(float modifiedMaxMana);
    // void SetModifiedSpellRange(float modifiedSpellRange);
    // void SetModifiedSpellDamage(float modifiedSpellDamage);
    // void SetModifiedSpellRecovery(float modifiedSpellRecovery);
    // void SetModifiedSpellStatusChance(float modifiedSpellStatusChance);
    public void SetModifiedDashingCooldown(float modifiedDashingCooldown);
}


public class PlayerStatsModifier : MonoBehaviour, IPlayerStatsModifier
{
    private PlayerAttributes _playerAttributes;
    
    public float GetModifiedMovementSpeed()
    {
        return _playerAttributes.MovementSpeed + _playerAttributes.MovementSpeedMultiplier;
    }  
    
    // public float GetModifiedMaxHealth()
    // {
    //     return _player.MaxHealth + _player.MaxHealthMultiplier;
    // } 
    //
    // public float GetModifiedMaxShield()
    // {
    //     return _player.MaxShield + _player.MaxShieldMultiplier;
    // } 
    //
    // public float GetModifiedAttackDamage()
    // {
    //     return _player.AttackDamage + _player.AttackDamageMultiplier;
    // } 
    //
    // public float GetModifiedAttackSpeed()
    // {
    //     return _player.AttackSpeed + _player.AttackSpeedMultiplier;
    // } 
    //
    // public float GetModifiedAttackRange()
    // {
    //     return _player.AttackRange + _player.AttackRangeMultiplier;
    // } 
    //
    // public float GetModifiedAttackStatusChance()
    // {
    //     return _player.AttackStatusChance + _player.AttackStatusChanceMultiplier;
    // } 
    //
    // public float GetModifiedMaxMana()
    // {
    //     return _player.MaxMana + _player.MaxManaMultiplier;
    // } 
    //
    // public float GetModifiedSpellRange(float spellRange)
    // {
    //     return spellRange + _player.SpellRangeMultiplier;
    // } 
    //
    // public float GetModifiedSpellDamage(float spellDamage)
    // {
    //     return spellDamage + _player.SpellDamageMultiplier;
    // } 
    //
    // public float GetModifiedSpellRecovery(float spellRecovery)
    // {
    //     return spellRecovery + _player.SpellRecoveryTimeMultiplier;
    // } 
    //
    // public float GetModifiedSpellStatusChance(float spellStatusChance)
    // {
    //     return spellStatusChance + _player.SpellStatusChanceMultiplier;
    // } 
    //
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
    //
    // public void SetModifiedMaxHealth(float modifiedMaxHealth)
    // {
    //     _player.MaxHealthMultiplier = modifiedMaxHealth;
    // } 
    //
    // public void SetModifiedMaxShield(float modifiedMaxShield)
    // {
    //     _player.MaxShieldMultiplier = modifiedMaxShield;
    // } 
    //
    // public void SetModifiedAttackDamage(float modifiedAttackDamage)
    // {
    //     _player.AttackDamageMultiplier = modifiedAttackDamage;
    // } 
    //
    // public void SetModifiedAttackSpeed(float modifiedAttackSpeed)
    // {
    //     _player.AttackSpeedMultiplier = modifiedAttackSpeed;
    // } 
    //
    // public void SetModifiedAttackRange(float modifiedAttackRange)
    // {
    //     _player.AttackRangeMultiplier = modifiedAttackRange;
    // } 
    //
    // public void SetModifiedAttackStatusChance(float modifiedAttackStatusChance)
    // {
    //     _player.AttackStatusChanceMultiplier = modifiedAttackStatusChance;
    // } 
    //
    // public void SetModifiedMaxMana(float modifiedMaxMana)
    // {
    //     _player.MaxManaMultiplier = modifiedMaxMana;
    // } 
    //
    // public void SetModifiedSpellRange(float modifiedSpellRange)
    // {
    //     _player.SpellRangeMultiplier = modifiedSpellRange;
    // } 
    //
    // public void SetModifiedSpellDamage(float modifiedSpellDamage)
    // {
    //     _player.SpellDamageMultiplier = modifiedSpellDamage;
    // } 
    //
    // public void SetModifiedSpellRecovery(float modifiedSpellRecovery)
    // {
    //     _player.SpellRecoveryTimeMultiplier = modifiedSpellRecovery;
    // } 
    //
    // public void SetModifiedSpellStatusChance(float modifiedSpellStatusChance)
    // {
    //     _player.SpellStatusChanceMultiplier = modifiedSpellStatusChance;
    // } 
    
    public void SetModifiedDashingCooldown(float modifiedDashingCooldown)
    {
        _playerAttributes.DashingCooldownMultiplier = modifiedDashingCooldown;
    }
}

using UnityEngine;

public interface IPlayerStatsModifier
{
    float GetModifiedMovementSpeed();
    float GetModifiedMaxHealth();
    float GetModifiedMaxShield();
    float GetModifiedAttackDamage();
    float GetModifiedAttackSpeed();
    float GetModifiedAttackRange();
    float GetModifiedAttackStatusChance();
    float GetModifiedMaxMana();
    float GetModifiedSpellRange(float spellRange);
    float GetModifiedSpellDamage(float spellDamage);
    float GetModifiedSpellRecovery(float spellRecovery);
    float GetModifiedDashingCooldown();
}

public class PlayerStatsModifier : MonoBehaviour, IPlayerStatsModifier
{
    private Player _player;
    
    public float GetModifiedMovementSpeed()
    {
        return _player.MovementSpeed + _player.MovementSpeedMultiplier;
    }  
    
    public float GetModifiedMaxHealth()
    {
        return _player.MaxHealth + _player.MaxHealthMultiplier;
    } 
    
    public float GetModifiedMaxShield()
    {
        return _player.MaxShield + _player.MaxShieldMultiplier;
    } 
    
    public float GetModifiedAttackDamage()
    {
        return _player.AttackDamage + _player.AttackDamageMultiplier;
    } 
    
    public float GetModifiedAttackSpeed()
    {
        return _player.AttackSpeed + _player.AttackSpeedMultiplier;
    } 
    
    public float GetModifiedAttackRange()
    {
        return _player.AttackRange + _player.AttackRangeMultiplier;
    } 
    
    public float GetModifiedAttackStatusChance()
    {
        return _player.AttackStatusChance + _player.AttackStatusChanceMultiplier;
    } 
    
    public float GetModifiedMaxMana()
    {
        return _player.MaxMana + _player.MaxManaMultiplier;
    } 
    
    public float GetModifiedSpellRange(float spellRange)
    {
        return spellRange + _player.SpellRangeMultiplier;
    } 
    
    public float GetModifiedSpellDamage(float spellDamage)
    {
        return spellDamage + _player.SpellDamageMultiplier;
    } 
    
    public float GetModifiedSpellRecovery(float spellRecovery)
    {
        return spellRecovery + _player.SpellRecoveryTimeMultiplier;
    } 
    
    public float GetModifiedSpellStatusChance(float spellStatusChance)
    {
        return spellStatusChance + _player.SpellStatusChanceMultiplier;
    } 
    
    public float GetModifiedDashingCooldown()
    {
        return _player.DashingCooldown + _player.DashingCooldownMultiplier;
    } 
}
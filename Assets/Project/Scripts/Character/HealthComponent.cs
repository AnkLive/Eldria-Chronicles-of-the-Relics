﻿using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IInitialize<HealthComponent>
{
    #region Fields

    private bool _isImmortalDuringThrow;
    private float _resistance;
    private float _effectiveDamage;
    private float _remainingShields;
    
    #endregion
    
    #region Inspector Fields
    
    [SerializeField] private bool isImmortal;
    [SerializeField] private bool hasShieldAbility;
    [SerializeField] private bool isImmortalDuringThrowAbility;
    [SerializeField, Range(0, 100)] private float maxHealth;
    [SerializeField, Range(0, 100)] private float currentHealth;
    [SerializeField, Range(0, 100)] private float armor;
    [SerializeField, Range(0, 100)] private float currentShield;
    [SerializeField, Range(0, 100)] private float maxShields;
    [SerializeField, Range(0, 100)] private float fireResistance;
    [SerializeField, Range(0, 100)] private float iceResistance;
    [SerializeField, Range(0, 100)] private float poisonResistance;

    public event Action OnCharacterDeath; 
    
    #endregion
    
    #region Methods
    
    public void Initialize()
    {
        currentHealth = maxHealth;
        if (hasShieldAbility)
        {
            maxShields = maxHealth / 2;
            currentShield = maxShields;
        }
    }
    
    public void TakeDamage(Damage damage)
    {
        if (isImmortal || _isImmortalDuringThrow) return;
        
        _resistance = 0f;
        
        switch (damage.StatusType)
        {
            case EStatusType.Fire:
                _resistance = fireResistance;
                break;
            case EStatusType.Ice:
                _resistance = iceResistance;
                break;
            case EStatusType.Poison:
                _resistance = poisonResistance;
                break;
            case EStatusType.Default:
                break;
        }


        _effectiveDamage = Mathf.Max(Mathf.RoundToInt(damage.damage - (damage.damage * (armor / 100f)) - (damage.damage * (_resistance / 100f))), 0);

        if (currentShield > 0)
        {
            _remainingShields = Mathf.Max(currentShield - _effectiveDamage, 0);
            currentHealth = Mathf.Max(currentHealth - Mathf.Max(_effectiveDamage - currentShield, 0), 0);
            currentShield = _remainingShields;
        }
        else
        {
            currentHealth = Mathf.Max(currentHealth - _effectiveDamage, 0);
        }

        if (currentHealth == 0f)
        {
            OnCharacterDeath?.Invoke();
        }
        Debug.LogWarning($"Получен урон {damage.damage} - текущее количество хп - {currentHealth}");
    }
    
    public void RestoreHealthInCheckpoint()
    {
        currentHealth = maxHealth;
        if (hasShieldAbility)
        {
            maxShields = maxHealth / 2;
            currentShield = maxShields;
        }
    }
    
    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
    }

    public void SetImmortalDuringThrow(bool isImmortal)
    {
        if (isImmortalDuringThrowAbility)
        {
            _isImmortalDuringThrow = isImmortal;
        }
    }
    
    #endregion
    
    #region SetFields

    public void SetFields(PlayerAttributes attributes)
    {
        isImmortalDuringThrowAbility = attributes.IsImmortalDuringThrowAbility;
        isImmortal = attributes.IsImmortal;
        maxHealth = attributes.MaxHealth + attributes.MaxHealthMultiplier;
        currentHealth = attributes.CurrentHealth;
        armor = attributes.Armor;
        hasShieldAbility = attributes.HasShieldAbility;
        fireResistance = attributes.FireResistance;
        iceResistance = attributes.IceResistance;
        poisonResistance = attributes.PoisonResistance;
    }
    
    public void GetFields(PlayerAttributes attributes)
    {
        attributes.IsImmortalDuringThrowAbility = isImmortalDuringThrowAbility;
        attributes.IsImmortal = isImmortal;
        attributes.MaxHealth = maxHealth;
        attributes.CurrentHealth = currentHealth;
        attributes.Armor = armor;
        attributes.HasShieldAbility = hasShieldAbility;
        attributes.FireResistance = fireResistance;
        attributes.IceResistance = iceResistance;
        attributes.PoisonResistance = poisonResistance;
    }

    #endregion
}
using System;
using UnityEngine;

public struct HealthComponentData
{
    public bool IsImmortal;
    public bool HasShieldAbility;
    public bool IsImmortalDuringThrowAbility;
    public float MaxHealth;
    public float Armor;
    public float FireResistance;
    public float IceResistance;
    public float PoisonResistance;
}

public class HealthComponent : MonoBehaviour, IInitialize<HealthComponent>
{
    #region Fields

    private bool _isImmortalDuringThrow;
    private float _resistance;
    private float _effectiveDamage;
    private float _remainingShields;
    private float _currentShield;
    private float _maxShields;
    private float _currentHealth;
    private HealthComponentData _healthComponentData;
    public event Action OnCharacterDeath; 
    
    #endregion
    
    #region Methods
    
    public void Initialize()
    {
        _currentHealth = _healthComponentData.MaxHealth;
        if (_healthComponentData.HasShieldAbility)
        {
            _maxShields = _healthComponentData.MaxHealth / 2;
            _currentShield = _maxShields;
        }
    }
    
    public void TakeDamage(Damage damage)
    {
        if (_healthComponentData.IsImmortal || _isImmortalDuringThrow) return;
        
        _resistance = 0f;
        
        switch (damage.statusType)
        {
            case EStatusType.Fire:
                _resistance = _healthComponentData.FireResistance;
                break;
            case EStatusType.Ice:
                _resistance = _healthComponentData.IceResistance;
                break;
            case EStatusType.Poison:
                _resistance = _healthComponentData.PoisonResistance;
                break;
            case EStatusType.Default:
                break;
        }


        _effectiveDamage = Mathf.Max(Mathf.RoundToInt(damage.damage - (damage.damage * (_healthComponentData.Armor / 100f)) - (damage.damage * (_resistance / 100f))), 0);

        if (_currentShield > 0)
        {
            _remainingShields = Mathf.Max(_currentShield - _effectiveDamage, 0);
            _currentHealth = Mathf.Max(_currentHealth - Mathf.Max(_effectiveDamage - _currentShield, 0), 0);
            _currentShield = _remainingShields;
        }
        else
        {
            _currentHealth = Mathf.Max(_currentHealth - _effectiveDamage, 0);
        }

        if (_currentHealth == 0f)
        {
            OnCharacterDeath?.Invoke();
        }
        Debug.LogWarning($"Получен урон {damage.damage} - текущее количество хп - {_currentHealth}");
    }
    
    public void RestoreHealthInCheckpoint()
    {
        _currentHealth = _healthComponentData.MaxHealth;
        if (_healthComponentData.HasShieldAbility)
        {
            _maxShields = _healthComponentData.MaxHealth / 2;
            _currentShield = _maxShields;
        }
    }
    
    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _healthComponentData.MaxHealth);
    }

    public void SetImmortalDuringThrow(bool isImmortal)
    {
        if (_healthComponentData.IsImmortalDuringThrowAbility)
        {
            _isImmortalDuringThrow = isImmortal;
        }
    }
    
    #endregion
    
    #region SetFields

    public void UpdateHealthComponentData(HealthComponentData data)
    {
        _healthComponentData = data;
    }

    public HealthComponentData GetHealthComponentData()
    {
        return _healthComponentData;
    }

    #endregion
}

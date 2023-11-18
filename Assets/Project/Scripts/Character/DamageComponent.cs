using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public struct DamageComponentData
{
    public bool CanAttack;
    public bool СanDealBodyDamageAbility;
    public EStatusType StatusType;
    public float AttackSpeed;
    public float AttackRange;
    public float AttackDamage;
    public float AttackDamageMultiplier;
    public float FireDamageMultiplier;
    public float IceDamageMultiplier;
    public float PoisonDamageMultiplier;
    public float CriticalChance;
    public float ElementalChance;
}

public class DamageComponent : MonoBehaviour
{
    #region Fields

    private float _finalDamage;
    private float _damage;
    
    #endregion
    
    #region Inspector Fields

    private DamageComponentData _damageComponentData;
    
    #endregion
    
    #region Methods
    
    public Damage ApplyDamage()
    {
        _finalDamage = CalculateDamage(_damageComponentData.StatusType);
        StartCoroutine(AttackCooldown());
        
        return new Damage(_finalDamage, _damageComponentData.StatusType);
    }

    private float CalculateDamage(EStatusType type)
    {
        _damage = _damageComponentData.AttackDamage;

        if (Random.Range(0.0f, 100.0f) < _damageComponentData.ElementalChance)
        {
            switch (type)
            {
                case EStatusType.Fire:
                    _damage += _damageComponentData.FireDamageMultiplier;
                    break;
                case EStatusType.Ice:
                    _damage += _damageComponentData.IceDamageMultiplier;
                    break;
                case EStatusType.Poison:
                    _damage += _damageComponentData.PoisonDamageMultiplier;
                    break;
            }
        }

        if (Random.Range(0.0f, 100.0f) < _damageComponentData.CriticalChance)
        {
            _damage += _damageComponentData.AttackDamageMultiplier;
        }
        
        return _damage;
    }
    
    private IEnumerator AttackCooldown()
    {
        _damageComponentData.CanAttack = false;
            
        yield return new WaitForSeconds(_damageComponentData.AttackSpeed);
        
        _damageComponentData.CanAttack = true;
    }
    
    #endregion
    
    #region SetFields

    public void UpdateDamageComponentData(DamageComponentData data)
    {
        _damageComponentData = data;
    }

    public DamageComponentData GetDamageComponentData()
    {
        return _damageComponentData;
    }
    
    #endregion
}

[Serializable]
public class Damage
{
    public Damage(float damage, EStatusType statusType)
    {
        this.damage = damage;
        this.statusType = statusType;
    }

    public float damage;
    public EStatusType statusType;
}
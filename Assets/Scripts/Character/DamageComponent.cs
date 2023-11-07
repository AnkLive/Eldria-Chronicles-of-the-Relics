using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class DamageComponent : MonoBehaviour
{
    #region Fields

    private float _finalDamage;
    private float _damage;
    
    #endregion
    
    #region Inspector Fields
    
    [field: SerializeField] public bool CanAttack { get; set; }
    [field: SerializeField] public bool СanDealBodyDamageAbility { get; set; }
    
    [SerializeField] private EStatusType statusType;
    [SerializeField, Range(0, 100)] private float attackSpeed;
    [SerializeField, Range(0, 100)] private float attackRange;
    [SerializeField, Range(0, 100)] private float attackDamage;
    [SerializeField, Range(0, 100)] private float attackDamageMultiplier;
    [SerializeField, Range(0, 100)] private float fireDamageMultiplier;
    [SerializeField, Range(0, 100)] private float iceDamageMultiplier;
    [SerializeField, Range(0, 100)] private float poisonDamageMultiplier;
    [SerializeField, Range(0, 100)] private float criticalChance;
    [SerializeField, Range(0, 100)] private float elementalChance;
    
    #endregion
    
    #region Methods
    
    public Damage ApplyDamage()
    {
        _finalDamage = CalculateDamage(statusType);
        StartCoroutine(AttackCooldown());
        
        return new Damage(_finalDamage, statusType);
    }

    private float CalculateDamage(EStatusType type)
    {
        _damage = attackDamage;

        if (Random.Range(0.0f, 100.0f) < elementalChance)
        {
            switch (type)
            {
                case EStatusType.Fire:
                    _damage += fireDamageMultiplier;
                    break;
                case EStatusType.Ice:
                    _damage += iceDamageMultiplier;
                    break;
                case EStatusType.Poison:
                    _damage += poisonDamageMultiplier;
                    break;
            }
        }

        if (Random.Range(0.0f, 100.0f) < criticalChance)
        {
            _damage += attackDamageMultiplier;
        }
        
        return _damage;
    }
    
    private IEnumerator AttackCooldown()
    {
        CanAttack = false;
            
        yield return new WaitForSeconds(attackSpeed);
        
        CanAttack = true;
    }
    
    #endregion
    
    #region SetFields

    public void SetFields(PlayerAttributes attributes)
    {
        СanDealBodyDamageAbility = attributes.СanDealBodyDamageAbility;
        attackDamage = attributes.AttackDamage;
        attackDamageMultiplier = attributes.AttackDamageMultiplier;
        fireDamageMultiplier = attributes.FireDamageMultiplier;
        iceDamageMultiplier = attributes.IceDamageMultiplier;
        poisonDamageMultiplier = attributes.PoisonDamageMultiplier;
        statusType = attributes.StatusType;
        criticalChance = attributes.CriticalChance;
        elementalChance = attributes.ElementalChance;
        CanAttack = attributes.CanAttack;
        attackSpeed = attributes.AttackSpeed + attributes.AttackSpeedMultiplier;
        
        attackRange = attributes.AttackRange;
    }
    
    public void GetFields(PlayerAttributes attributes)
    {
        attributes.СanDealBodyDamageAbility = СanDealBodyDamageAbility;
        attributes.AttackDamage = attackDamage;
        attributes.AttackDamageMultiplier = attackDamageMultiplier;
        attributes.FireDamageMultiplier = fireDamageMultiplier;
        attributes.IceDamageMultiplier = iceDamageMultiplier;
        attributes.PoisonDamageMultiplier = poisonDamageMultiplier;
        attributes.StatusType = statusType;
        attributes.CriticalChance = criticalChance;
        attributes.ElementalChance = elementalChance;
        attributes.CanAttack = CanAttack;
        attributes.AttackSpeed = attackSpeed;
        
        attributes.AttackRange = attackRange;
    }

    #endregion
}

[Serializable]
public class Damage
{
    public Damage(float damage, EStatusType eStatusType)
    {
        this.damage = damage;
        this.StatusType = eStatusType;
    }

    public float damage;
    public EStatusType StatusType;
}
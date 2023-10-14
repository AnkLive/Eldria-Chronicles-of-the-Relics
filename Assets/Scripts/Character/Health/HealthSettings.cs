using UnityEngine;

public class HealthSettings : MonoBehaviour, IInitialize
{
    public float MaxHealth;
    public float CurrentHealth;
    public bool IsDead;
    public float MaxShield;
    public float CurrentShield;
    public bool IsShield;
    
    public void Initialize()
    {
        ReplenishHealth(CurrentHealth, MaxHealth);
    }
    
    public void TakeDamage(float damage)
    {
        if (IsShield) 
        {
            MaxShield -= damage;

            if (CurrentShield <= 0) 
            {
                IsShield = false;
            }
        }
        else 
        {
            CurrentHealth -= damage;
        }

        if (CurrentHealth <= 0) 
        {
            IsDead = true;
        }
    }
    
    public void ReplenishHealth(float currentHealth, float maxHealth) => CurrentHealth = MaxHealth;
    
    public void ReplenishShield(float currentShield, float maxShield) => CurrentHealth = MaxShield;
}

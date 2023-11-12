using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(DamageComponent))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField, Tag] private string detectObjectTag;
    
    private DamageComponent _damageComponent;
    private HealthComponent _healthComponent;
    
    private void Start()
    {
        _damageComponent = gameObject.GetComponent<DamageComponent>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(detectObjectTag))
        {
            _healthComponent = other.gameObject.GetComponent<HealthComponent>();
            
            if (_healthComponent != null)
            {
                if (_damageComponent.СanDealBodyDamageAbility)
                {
                    LaunchAnAttack();
                }
                return;
            }
            Debug.LogError($"Ошибка [CollisionHandler] - не найден компонент HealthComponent при попытке нанести урон");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _healthComponent = null;
    }

    public void LaunchAnAttack()
    {
        if (_healthComponent != null)
        {
            if (_damageComponent.CanAttack)
            {
                _healthComponent.TakeDamage(_damageComponent.ApplyDamage());
            }
        }
    }
}
using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(DamageComponent))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField, Tag] private string detectObjectTag;
    
    private DamageComponent _damageComponent;
    
    private void Start()
    {
        _damageComponent = gameObject.GetComponent<DamageComponent>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag(detectObjectTag))
        {
            var health = other.gameObject.GetComponent<HealthComponent>();
            
            if (health != null)
            {
                if (_damageComponent.CanAttack)
                {
                    health.TakeDamage(_damageComponent.ApplyDamage());
                    return;
                }
                return;
            }
            Debug.LogError($"Ошибка [CollisionHandler] - не найден компонент HealthComponent при попытке нанести урон");
        }
    }
}
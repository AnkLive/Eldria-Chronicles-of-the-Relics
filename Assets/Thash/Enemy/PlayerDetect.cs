using UnityEngine;

namespace Platformer.EnemyState
{
    public class PlayerDetect : MonoBehaviour
    {
        public delegate void OnTriggerEnter(bool isTrigger, GameObject obj);
        public event OnTriggerEnter TriggerEvent; 

        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.CompareTag("Player"))
                TriggerEvent?.Invoke(true, other.gameObject);
        }

        private void OnTriggerExit2D(Collider2D other) 
        {
            if(other.CompareTag("Player"))
                TriggerEvent?.Invoke(false, null);
        }
    }
}
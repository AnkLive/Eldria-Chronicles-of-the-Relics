using UnityEngine;

namespace Platformer.EnemyState
{
    [CreateAssetMenu(fileName = "Player Ranged Attack State", menuName = "Scriptable Object/States/Player Ranged Attack State", order = 0)]
    public class PlayerRangedAttackState : State
    {
        private float _timer;
        
        [field:SerializeField] public float DelayTime { get; private set; }
        [field:SerializeField] public bool IsPressed  { get; private set; }
        
        public override void Init() 
            => _timer = DelayTime;
        
        public override void Run()
        {
            if(IsFinished)
                return;

            if(!EnemyState.PlayerDetected || EnemyState.DetectObj == null)
            {
                IsFinished = true;
                EnemyState.SetState(EnemyState.RandomMoveState);
            }
            else
            {
                _timer += Time.deltaTime;

                if (_timer >= DelayTime || IsPressed)
                {
                    EnemyState.FlyingObject.CreateNewPrefab(EnemyState.DetectObj.transform.position);
                    _timer = 0;
                }
            }
        }
    }
}
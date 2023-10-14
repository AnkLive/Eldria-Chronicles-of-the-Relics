using UnityEngine;

namespace Platformer.EnemyState
{
    
    [CreateAssetMenu(fileName = "Player Attack State", menuName = "Scriptable Object/States/Player Attack State", order = 0)]
    public class PlayerAttackState : State
    {
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
                EnemyState.MoveToPosition(EnemyState.DetectObj.transform.position);
        }
    }
}

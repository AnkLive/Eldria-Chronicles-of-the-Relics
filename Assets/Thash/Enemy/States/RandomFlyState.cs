using UnityEngine;

namespace Platformer.EnemyState
{
    [CreateAssetMenu(fileName = "Random Move State", menuName = "Scriptable Object/States/Random Fly State", order = 0)]
    public class RandomFlyState : State
    {
        private bool _isNextPosition = true;
        private Vector2 _nextPosition;
        private Vector2 _startPosition;
        private float _delayTimer;
        
        [field:SerializeField] private float RangeMove     { get; set; }
        [field:SerializeField] private float DelayDuration { get; set; }
        

        public override void Init()
        {
            //_startPosition = EnemyState.RigidbodyObject.position;
            SetNextPosition();
        }

        public override void Run()
        {
            if (IsFinished)
                return;

            if (EnemyState.PlayerDetected)
            {
                IsFinished = true;
                EnemyState.SetState(EnemyState.PlayerAttackState);
                return;
            }

            if (_delayTimer > 0f)
            {
                _delayTimer -= Time.deltaTime;
                return;
            }

            if (_isNextPosition)
                SetNextPosition();

            //_isNextPosition = EnemyState.MoveToPosition(_nextPosition);
        }

        private void SetNextPosition()
        {
            _nextPosition = _startPosition + Random.insideUnitCircle * RangeMove;
            _delayTimer = DelayDuration;
        }
    }
}

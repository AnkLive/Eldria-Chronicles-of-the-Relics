using UnityEngine;

namespace Platformer.EnemyState
{
    [CreateAssetMenu(fileName = "Random Move State", menuName = "Scriptable Object/States/Random Move State", order = 0)]
    public class RandomMoveState : State
    {
        private bool _isNextPosition = true;
        private float _nextPosition;
        
        [field:SerializeField] public float LeftAnchor  { get; set; }
        [field:SerializeField] public float RightAnchor { get; set; }
        
        public override void Init() 
            => _nextPosition = RightAnchor;

        public override void Run()
        {
            if(_isNextPosition)
                SetNextPosition();

            //_isNextPosition = EnemyState.MoveToPosition(new Vector2(_nextPosition, EnemyState.RigidbodyObject.position.y));
        }
        
        public void SetNextPosition() => _nextPosition = (_nextPosition == RightAnchor) ? LeftAnchor : RightAnchor;
    }
}

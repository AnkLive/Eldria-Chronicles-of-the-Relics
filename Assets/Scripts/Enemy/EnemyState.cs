using Platformer.MovementSystem;
using UnityEngine;

namespace Platformer.EnemyState
{
    public class EnemyState : Movement
    {
        private State CurrentState;
        
        public InitializeFlyingObject FlyingObject                { get; set; }
        
        public GameObject DetectObj                               { get; set; }
        public bool PlayerDetected                                { get; private set; }
        
        [field: Header("Настройки состояний врага")]
        [field:SerializeField] public bool RangedAttack           { get; private set; }
        
        [field:SerializeField] public State StartState            { get; private set; }
        [field:SerializeField] public State PlayerAttackState     { get; private set; }
        [field:SerializeField] public State RandomMoveState       { get; private set; }
        
        [field:SerializeField] public PlayerDetect DetectCollider { get; private set; }

        public override void Initialize()
        {
            base.Initialize();
            
            DetectCollider.TriggerEvent += Detect;
            SetState(StartState);
            
            if (RangedAttack)
                FlyingObject = GetComponent<InitializeFlyingObject>();
        }

        private void Update()
        {
            if(!CurrentState.IsFinished)
                CurrentState.Run();
            else
                CurrentState = PlayerDetected ? PlayerAttackState : RandomMoveState;
        }

        public void SetState(State state)
        {
            CurrentState = Instantiate(state);
            CurrentState.EnemyState = this;
            CurrentState.Init();
        }

        private void Detect(bool value, GameObject obj) 
        {
            PlayerDetected = value;
            DetectObj = obj;
        }

        private void OnDestroy() 
            => DetectCollider.TriggerEvent -= Detect;
    }
}

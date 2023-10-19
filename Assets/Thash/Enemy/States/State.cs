using UnityEngine;

namespace Platformer.EnemyState
{
    public abstract class State : ScriptableObject
    {
        public bool IsFinished { get; protected set; }
        [HideInInspector] public EnemyState EnemyState;

        public virtual void Init() { }
        
        public abstract void Run();
    }
}

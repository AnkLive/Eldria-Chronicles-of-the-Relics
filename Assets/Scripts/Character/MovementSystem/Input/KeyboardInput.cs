using UnityEngine;

namespace Platformer.MovementSystem
{
    public class KeyboardInput : BaseInput
    {
        [SerializeField] private GlobalStringVars _globalStringVars;
        public override InputData GenerateInput() 
        {
            return new InputData
            {
                MoveToLeft = Input.GetKey(_globalStringVars.GetVars("MOVE_TO_LEFT")),
                MoveToRight = Input.GetKey(_globalStringVars.GetVars("MOVE_TO_RIGHT")),
                IsJumped = Input.GetKeyDown(_globalStringVars.GetVars("JUMP")),
                Jumped = Input.GetKeyUp(_globalStringVars.GetVars("JUMP")),
                Dash = Input.GetKeyDown(_globalStringVars.GetVars("DASH"))
            };
        }
    }
}
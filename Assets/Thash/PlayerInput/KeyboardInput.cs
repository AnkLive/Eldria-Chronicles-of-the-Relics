using UnityEngine;
using UnityEngine.Serialization;

    public class KeyboardInput : PlayerInput
    {
        [SerializeField] private StringVariableManager stringVariableManager;
        public override InputData GenerateInput() 
        {
            return new InputData
            {
                MoveToLeft = Input.GetKey(stringVariableManager.GetVars("MOVE_TO_LEFT")),
                MoveToRight = Input.GetKey(stringVariableManager.GetVars("MOVE_TO_RIGHT")),
                IsJumped = Input.GetKeyDown(stringVariableManager.GetVars("JUMP")),
                Jumped = Input.GetKeyUp(stringVariableManager.GetVars("JUMP")),
                Dash = Input.GetKeyDown(stringVariableManager.GetVars("DASH"))
            };
        }
    }
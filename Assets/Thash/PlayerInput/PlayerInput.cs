using UnityEngine;

public abstract class PlayerInput : MonoBehaviour, IInput 
{ 
    public abstract InputData GenerateInput();
}

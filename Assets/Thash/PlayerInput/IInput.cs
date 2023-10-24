public struct InputData
{
    public bool MoveToLeft;
    public bool MoveToRight;
    public bool Jumped;
    public bool IsJumped;
    public bool Dash;
}

public interface IInput
{
    InputData GenerateInput();
}

public interface IInputInitialize
{
    public void GatherInputs();
    public void SetInputData();
}
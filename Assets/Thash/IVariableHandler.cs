using UnityEngine;

public interface IVariableHandler
{
    public void SetVars(StringVariableManager data);

    public KeyCode GetVars(string keyCodeString);
}
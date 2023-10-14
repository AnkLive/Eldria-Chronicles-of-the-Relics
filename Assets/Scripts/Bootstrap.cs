using Platformer.EnemyState;
using Platformer.MovementSystem;
using UnityEngine;

public interface IInitialize
{
    public void Initialize();
}

public class Bootstrap : MonoBehaviour
{
    public Movement playerMovement;
    public GlobalStringVars itemStorage;
    public EnemyState[] enemyState;
    private void Awake() 
    {
        playerMovement.Initialize();
        //Instantiate(itemStorage);
        //itemStorage.Initialize();
        foreach (var item in enemyState)
        {
            item.Initialize();
        }
    }
}

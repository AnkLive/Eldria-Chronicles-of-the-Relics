using UnityEngine;
using Zenject;



public class Bootstrap : MonoBehaviour
{
    [Inject]
    private DiContainer container;

    private void Awake()
    {
        container.Resolve<ProjectContext>();
    }
}

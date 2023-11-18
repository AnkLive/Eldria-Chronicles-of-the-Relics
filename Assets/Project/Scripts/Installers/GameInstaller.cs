using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InventoryManager>().To<InventoryManager>().FromComponentInHierarchy().AsCached();
        Container.Bind<InventoryUIManager>().To<InventoryUIManager>().FromComponentInHierarchy().AsCached();
    }
}
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(ISaveLoader<StringVariableManager>)).To<InputVarsSaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(ISaveLoader<Inventory>)).To<InventorySaveLoader>().FromComponentInHierarchy().AsCached(); 
        Container.Bind<InventoryManager>().To<InventoryManager>().FromComponentInHierarchy().AsCached();
        Container.Bind<InventoryUIManager>().To<InventoryUIManager>().FromComponentInHierarchy().AsCached();
        Container.Bind<SwapItems>().To<SwapItems>().FromComponentInHierarchy().AsCached();
    }
}
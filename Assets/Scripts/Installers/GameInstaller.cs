using InventorySystem;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(IInitialize<InventorySaveLoader>)).To<InventorySaveLoader>().FromComponentInHierarchy().AsSingle();
        Container.Bind(typeof(IInitialize<InputVarsSaveLoader>)).To<InputVarsSaveLoader>().FromComponentInHierarchy().AsSingle();
        Container.Bind(typeof(IInitialize<PlayerStatsSaveLoader>)).To<PlayerStatsSaveLoader>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind(typeof(ISaveLoader<StringVariableManager>)).To<InputVarsSaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(ISaveLoader<Inventory>)).To<InventorySaveLoader>().FromComponentInHierarchy().AsCached();
        
        Container.Bind(typeof(IInitialize<InventoryManager>)).To<InventoryManager>().FromComponentInHierarchy().AsCached();
    }
}
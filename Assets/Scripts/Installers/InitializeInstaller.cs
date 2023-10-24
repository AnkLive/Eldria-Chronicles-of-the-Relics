using Zenject;

public interface IActivate<T>
{
    public void Activate();
    public void Deactivate();
}

public interface IInitialize<T>
{
    public void Initialize();
}

public class InitializeInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(IInitialize<InventorySaveLoader>)).To<InventorySaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(IInitialize<InputVarsSaveLoader>)).To<InputVarsSaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(IInitialize<PlayerStatsSaveLoader>)).To<PlayerStatsSaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(IInitialize<InventoryManager>)).To<InventoryManager>().FromComponentInHierarchy().AsCached();
    }
}
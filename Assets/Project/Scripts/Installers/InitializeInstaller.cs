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
        Container.Bind(typeof(IInitialize<InventoryManager>)).To<InventoryManager>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(IInitialize<InventoryUIManager>)).To<InventoryUIManager>().FromComponentInHierarchy().AsCached();
        
        Container.Bind(typeof(IActivate<InventoryManager>)).To<InventoryManager>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(IActivate<InventoryUIManager>)).To<InventoryUIManager>().FromComponentInHierarchy().AsCached();
    }
}
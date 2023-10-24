using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(ISaveLoader<StringVariableManager>)).To<InputVarsSaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(ISaveLoader<Inventory>)).To<InventorySaveLoader>().FromComponentInHierarchy().AsCached();
    }
}
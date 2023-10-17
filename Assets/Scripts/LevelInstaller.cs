using Platformer.MovementSystem;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<InventoryManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        
        //Container.Bind(typeof(IInitialize<InventoryManager>)).To<InventoryManager>().FromComponentInHierarchy().AsCached();
    }
}

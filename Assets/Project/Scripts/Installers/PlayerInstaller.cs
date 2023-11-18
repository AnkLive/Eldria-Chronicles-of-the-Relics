using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(IInitialize<PlayerUnitMovementComponent>)).To<PlayerUnitMovementComponent>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(IInitialize<HealthComponent>)).To<HealthComponent>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(IActivate<PlayerUnitMovementComponent>)).To<PlayerUnitMovementComponent>().FromComponentInHierarchy().AsCached();
        
        Container.Bind<PlayerUnitMovementComponent>().To<PlayerUnitMovementComponent>().FromComponentInHierarchy().AsCached();
        Container.Bind<HealthComponent>().To<HealthComponent>().FromComponentInHierarchy().AsCached();
        Container.Bind<DamageComponent>().To<DamageComponent>().FromComponentInHierarchy().AsCached();
        
    }
}
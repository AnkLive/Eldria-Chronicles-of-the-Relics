using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind(typeof(ISaveLoader<PlayerAttributes>)).To<PlayerStatsSaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind<IPlayerStatsModifier>().To<PlayerStatsModifier>().FromComponentInHierarchy().AsSingle();
    }
}
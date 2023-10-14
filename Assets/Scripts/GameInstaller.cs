using Platformer.MovementSystem;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IPlayerStatsModifier>().To<PlayerStatsModifier>().FromComponentInHierarchy().AsSingle();
    }
}
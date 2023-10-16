using Platformer.MovementSystem;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // Свяжем Movement с PlayerStatsModifier
        Container.Bind<Movement>().FromComponentInHierarchy().AsSingle();
        //Container.Bind<IPlayerStatsModifier>().To<PlayerStatsModifier>().AsSingle();
        // Другие зависимости уровня
    }
}

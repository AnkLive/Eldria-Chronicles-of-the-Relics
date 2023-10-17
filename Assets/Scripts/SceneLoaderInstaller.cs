using Zenject;

public class SceneLoaderInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SceneLoader>().FromComponentInHierarchy().AsSingle();
    }
}
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind(typeof(IInitialize<InventorySaveLoader>)).To<InventorySaveLoader>().FromComponentInHierarchy().AsSingle();
        Container.Bind(typeof(IInitialize<InputVarsSaveLoader>)).To<InputVarsSaveLoader>().FromComponentInHierarchy().AsSingle();
        Container.Bind(typeof(IInitialize<PlayerStatsSaveLoader>)).To<PlayerStatsSaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind(typeof(ISaveLoader<Player>)).To<PlayerStatsSaveLoader>().FromComponentInHierarchy().AsCached();
        Container.Bind<IPlayerStatsModifier>().To<PlayerStatsModifier>().AsSingle().NonLazy();
        Debug.LogWarning("1131314");
    }
}
using UnityEngine;
using Zenject;

public class BootstrapSceneInstaller : MonoInstaller<BootstrapSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IGameBootstraper>().To<GameBootstraper>().AsSingle().NonLazy();
    }
}
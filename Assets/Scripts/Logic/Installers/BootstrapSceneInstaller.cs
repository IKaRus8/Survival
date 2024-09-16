using UnityEngine;
using Zenject;

public class BootstrapSceneInstaller : MonoInstaller<BootstrapSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IServicesLoader>().To<ServicesLoader>().AsSingle().NonLazy();
    }
}
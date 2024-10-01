using Logic.Services;
using Zenject;

namespace Logic.Installers
{
    public class BootstrapSceneInstaller : MonoInstaller<BootstrapSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameBootstraper>().To<GameBootstraper>().AsSingle().NonLazy();
        }
    }
}
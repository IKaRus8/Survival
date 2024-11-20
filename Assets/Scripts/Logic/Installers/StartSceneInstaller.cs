using Logic.Services;
using Zenject;

namespace Logic.Installers
{
    public class StartSceneInstaller : MonoInstaller<StartSceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<GameBootstrapper>().AsSingle().NonLazy();
        }
    }
}
using Data;
using Data.Interfaces;
using Logic.Services;
using Zenject;

namespace Logic.Installers
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        public override void InstallBindings()
        {
            // Services
            Container.BindInterfacesTo<AssetService>().AsSingle();   
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IGameEntities>().To<GameEntities>().AsSingle();
        }
    }
}
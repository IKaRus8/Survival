using Logic.Interfaces;
using Logic.Services;
using Zenject;

namespace Logic.Installers
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        public override void InstallBindings()
        {
            // Services
            Container.Bind<IAssetService>().To<AssetService>().AsSingle();   
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}
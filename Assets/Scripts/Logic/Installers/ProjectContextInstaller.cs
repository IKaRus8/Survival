using Zenject;

namespace Logic.Installers
{
    public class ProjectContextInstaller : MonoInstaller<ProjectContextInstaller>
    {
        public override void InstallBindings()
        {
            // Services
            Container.Bind<IAssetService>().To<AssetService>().AsSingle();
            Container.Bind<IInput>().To<MobileInput>().AsSingle();
        }
    }
}
using Data;
using Data.Interfaces;
using Logic.Interfaces.Services;
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
            Container.Bind<IGameSettings>().To<GameSettings>().AsSingle();
            Container.Bind<IPopupSystem>().To<PopupSystem>().AsSingle();
        }
    }
}
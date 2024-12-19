using Logic.Interfaces.Presenters;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Providers.Level.Hero;
using Logic.Interfaces.Providers.Level.Projectiles;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Services.Level.Projectiles;
using Logic.Presenters;
using Logic.Providers.Level;
using Logic.Providers.Level.Enemies;
using Logic.Providers.Level.Hero;
using Logic.Providers.Level.Projectiles;
using Logic.Services.Input;
using Logic.Services.Level;
using Logic.Services.Level.Attack;
using Logic.Services.Level.Enemy;
using Logic.Services.Level.Hero;
using Logic.Services.Level.Projectiles;
using Logic.Services.Level.Survival;
using Logic.Services.Level.Survival.Enemy;
using Logic.Services.Level.Survival.Hero;
using Zenject;

namespace Logic.Installers
{
    public class CommonLevelInstaller : Installer<CommonLevelInstaller>
    {
        public override void InstallBindings()
        {
            // Scene objects 
            Container.Bind<Joystick>().FromComponentInHierarchy().AsSingle();

            // Services
            Container.Bind<IHeroSpawner>().To<HeroCreator>().AsTransient();
            Container.BindInterfacesTo<MobileInput>().AsSingle();
            Container.BindInterfacesTo<HeroMoveSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroRotateSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraMovementSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DamageSystem>().AsSingle().NonLazy();
            Container.Bind<IProjectileFabric>().To<ProjectileFabric>().AsTransient();
            Container.Bind<IProjectileDamageSystem>().To<ProjectileDamageSystem>().AsTransient();
            Container.Bind<IPauseService>().To<PauseService>().AsSingle();
            Container.Bind<IVfxService>().To<VfxService>().AsSingle();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsTransient();
            Container.BindInterfacesTo<ProjectilesCollisionSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerTargetObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroAttackService>().AsSingle().NonLazy();

            // Providers
            Container.BindInterfacesTo<EnemyProvider>().AsSingle();
            Container.Bind<IEnemyModelsProvider>().To<EnemyModelsProvider>().AsSingle();
            Container.Bind<IHeroModelsProvider>().To<HeroModelsProvider>().AsSingle();
            Container.Bind<IProjectilesProvider>().To<ProjectilesProvider>().AsSingle();
            Container.Bind<IAttackModelsProvider>().To<AttackModelsProvider>().AsSingle();

            //Presenters
            Container.Bind<IGameEndedPopupPresenter>().To<GameEndedPopupPresenter>().AsSingle();
            Container.Bind<ISettingsPopupPresenter>().To<SettingsPopupPresenter>().AsSingle();
        }
    }
}
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Providers.Level.Hero;
using Logic.Interfaces.Providers.Level.Projectiles;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
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
using Logic.Services.Level.Grid;
using Logic.Services.Level.Hero;
using Logic.Services.Level.Pools;
using Logic.Services.Level.Projectiles;
using Logic.Unity.Projectiles;
using Logic.Unity.SceneObjects;
using UnityEngine;
using Zenject;

namespace Logic.Installers
{
    public class LevelSceneInstaller : MonoInstaller<LevelSceneInstaller>
    {
        [SerializeField]
        private Joystick _joystick;
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private LevelSceneObjectsContainer _levelSceneObjectsContainer;
        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private Transform _bulletsParent;

        public override void InstallBindings()
        {
            // Scene objects 
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<ILevelSceneObjectContainer>().FromInstance(_levelSceneObjectsContainer).AsSingle();

            // Services
            Container.BindInterfacesTo<MobileInput>().AsSingle();
            Container.Bind<IHeroSpawner>().To<HeroCreator>().AsTransient();
            Container.BindInterfacesTo<HeroHolder>().AsSingle();
            Container.BindInterfacesTo<HeroMoveSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroRotateSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraMovementSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GridSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DamageSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyStatesObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroDetectedService>().AsSingle().NonLazy();
            Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsTransient();
            Container.Bind<IProjectileFabric>().To<ProjectileFabric>().AsTransient();
            Container.BindInterfacesTo<EnemyCollisionSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ProjectilesCollisionSystem>().AsSingle().NonLazy();
            Container.Bind<IProjectileDamageSystem>().To<ProjectileDamageSystem>().AsTransient();

            // Providers
            Container.Bind<IEnemySpawnSettingsProvider>().To<EnemySpawnSettingsProvider>().AsSingle();
            Container.BindInterfacesTo<EnemyProvider>().AsSingle();
            Container.BindInterfacesTo<PlayerTargetObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroAttackService>().AsSingle().NonLazy();
            Container.Bind<IEnemyModelsProvider>().To<EnemyModelsProvider>().AsSingle();
            Container.Bind<IHeroModelsProvider>().To<HeroModelsProvider>().AsSingle();
            Container.BindInterfacesTo<RectanglesProvider>().AsSingle();
            Container.Bind<IProjectilesProvider>().To<ProjectilesProvider>().AsSingle();
            Container.Bind<IAttackModelsProvider>().To<AttackModelsProvider>().AsSingle();

            // Pools
            Container.BindMemoryPool<Projectile, ProjectilesPool>()
                .WithInitialSize(10) // Начальный размер пула
                .FromComponentInNewPrefab(_bulletPrefab) // Префаб пули
                .UnderTransform(_bulletsParent);

            //Presenters
            Container.Bind<IGameEndedPopupPresenter>().To<GameEndedPopupPresenter>().AsSingle();
        }
    }
}
using Logic.Interfaces;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Providers.Enemies;
using Logic.Presenters;
using Logic.Providers.Enemies;
using Logic.Services.Input;
using Logic.Services.Level;
using Logic.Services.Level.Enemy;
using Logic.Services.Player;
using Logic.Services.Pools;
using Logic.Unity;
using Logic.Unity.Weapon;
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
        GameObject _bulletPrefab;

        public override void InstallBindings()
        {
            // Scene objects 
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<ILevelSceneObjectContainer>().FromInstance(_levelSceneObjectsContainer).AsSingle();

            // Services
            Container.Bind<IInput>().To<MobileInput>().AsSingle();
            Container.Bind<ICreator<IPlayer>>().To<PlayerCreator>().AsSingle();
            Container.BindInterfacesTo<PlayerHolder>().AsSingle();
            Container.BindInterfacesTo<PlayerMoveSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerRotateSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CameraMovementSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GridSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemySpawner>().AsSingle().NonLazy();
            Container.Bind<EnemyDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyMoveSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyAttackSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DamageSystem>().AsSingle().NonLazy();
            Container.Bind<PlayerDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyStatesObserver>().AsSingle().NonLazy();

            // Providers
            Container.Bind<IEnemySpawnSettingsProvider>().To<EnemySpawnSettingsProvider>().AsSingle();
            Container.BindInterfacesTo<EnemyProvider>().AsSingle();
            Container.BindInterfacesTo<PlayerTargetObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerShootService>().AsSingle().NonLazy();
            Container.Bind<IEnemyModelsProvider>().To<EnemyModelsProvider>().AsSingle();

            // Pools
            Container.BindMemoryPool<Bullet, BulletPool>()
                .WithInitialSize(10) // Начальный размер пула
                .FromComponentInNewPrefab(_bulletPrefab) // Префаб пули
                .UnderTransformGroup("Bullets");

            //Presenters
            Container.Bind<IGameEndedPopupPresenter>().To<GameEndedPopupPresenter>().AsSingle();
        }
    }
}
using Logic.Interfaces;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Providers;
using Logic.Presenters;
using Logic.Providers;
using Logic.Services;
using Logic.Services.Input;
using Logic.Weapon;
using UnityEngine;
using Zenject;

namespace Logic.Installers
{
    public class SurvivalSceneInstaller : MonoInstaller<SurvivalSceneInstaller>
    {
        [SerializeField]
        private Joystick _joystick;
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private SceneObjectContainer _sceneObjectContainer;  
        [SerializeField] 
        GameObject _bulletPrefab;

        public override void InstallBindings()
        {
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
            // Scene objects 
            Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.Bind<ISceneObjectContainer>().FromInstance(_sceneObjectContainer).AsSingle();
        
            // Providers
            Container.Bind<IEnemySpawnSettingsProvider>().To<EnemySpawnSettingsProvider>().AsSingle();
            Container.BindInterfacesTo<AliveEnemyProvider>().AsSingle();
            Container.BindInterfacesTo<PlayerTargetObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<PlayerShootService>().AsSingle().NonLazy();

            Container.BindFactory<IPlayer, IEnemy, Transform, IDamageSystem, Bullet, Bullet.Factory>().FromMonoPoolableMemoryPool(
                x => x.WithInitialSize(30).FromComponentInNewPrefab(_bulletPrefab).UnderTransformGroup("BulletPool"));
        
            //Presenters
            Container.Bind<IGameEndedPopupPresenter>().To<GameEndedPopupPresenter>().AsSingle();
        }
    }
}

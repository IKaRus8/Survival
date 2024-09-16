using Logic.Interfaces.Providers;
using Logic.Providers;
using Logic.Services;
using UnityEngine;
using Zenject;

public class CommonZenjectInstaller : MonoInstaller<CommonZenjectInstaller>
{
    [SerializeField]
    private Joystick _joystick;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private SceneObjectContainer _sceneObjectContainer;  
    [SerializeField] GameObject _bulletPrefab;

    public override void InstallBindings()
    {
        // Services
        Container.Bind<IInput>().To<MobileInput>().AsSingle();
        Container.Bind<ICreator<IPlayer>>().To<PlayerCreator>().AsSingle();
        Container.BindInterfacesTo<PlayerHolder>().AsSingle();
        Container.BindInterfacesTo<PlayerMoveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesTo<PlayerRotateSystem>().AsSingle().NonLazy();
        Container.BindInterfacesTo<CameraMoveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesTo<GridController>().AsSingle().NonLazy();
        Container.BindInterfacesTo<EnemySpawner>().AsSingle().NonLazy();
        Container.Bind<EnemyDeathObserver>().AsSingle().NonLazy();
        Container.BindInterfacesTo<EnemyMoveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesTo<EnemyAttackSystem>().AsSingle().NonLazy();

        // Scene objects 
        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        Container.Bind<ISceneObjectContainer>().FromInstance(_sceneObjectContainer).AsSingle();
        
        // Providers
        Container.Bind<IEnemySpawnSettingsProvider>().To<EnemySpawnSettingsProvider>().AsSingle();
        Container.BindInterfacesTo<AliveEnemyProvider>().AsSingle();
        Container.BindInterfacesTo<PlayerTargetsProvider>().AsSingle().NonLazy();
        Container.BindInterfacesTo<PlayerWeapon>().AsSingle().NonLazy();

        Container.BindFactory<IEnemy, Transform, Bullet, Bullet.Factory>().FromMonoPoolableMemoryPool(
            x => x.WithInitialSize(30).FromComponentInNewPrefab(_bulletPrefab).UnderTransformGroup("BulletPool"));
    }
}

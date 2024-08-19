using Logic.Services;
using UnityEngine;
using Zenject;

public class CommonZenjectInstaller : MonoInstaller
{  
    [SerializeField] 
    private Joystick _joystick;  
    [SerializeField] 
    private Camera _camera;
    [SerializeField]
    private SceneObjectContainer _sceneObjectContainer;
    [SerializeField]
    private Enemy _enemyPrefab;
    
    public override async void InstallBindings()
    {
        Container.Bind<IAssetService>().To<AssetService>().AsSingle();
        Container.Bind<ICreator<IPlayer>>().To<PlayerCreator>().AsSingle();
        Container.Bind<IPlayer>().To<Player>().AsSingle();
        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        Container.Bind<IInput>().To<MobileInput>().AsSingle();
        Container.BindInterfacesTo<PlayerHolder>().AsSingle();      
        Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        Container.BindInterfacesTo<PlayerMoveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesTo<CameraMoveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesTo<RoadController>().AsSingle().NonLazy();
        Container.Bind<ISceneObjectContainer>().FromInstance(_sceneObjectContainer).AsSingle();
        Container.BindInterfacesTo<EnemySpawer>().AsSingle().NonLazy();
        Container.BindMemoryPool<Enemy, Enemy.Pool>()
            .WithInitialSize(30)
            .FromComponentInNewPrefab(_enemyPrefab)
            .UnderTransformGroup("Enemies");
    }
}

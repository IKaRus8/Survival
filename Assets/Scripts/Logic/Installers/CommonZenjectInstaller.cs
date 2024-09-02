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
    [SerializeField] 
    private GameObject _enemyPrefab;
    
    public override void InstallBindings()
    {
        // Services
        Container.Bind<IInput>().To<MobileInput>().AsSingle();
        Container.Bind<ICreator<IPlayer>>().To<PlayerCreator>().AsSingle();
        Container.BindInterfacesTo<PlayerHolder>().AsSingle();
        Container.BindInterfacesTo<PlayerMoveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesTo<CameraMoveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesTo<GridController>().AsSingle().NonLazy();
        Container.BindInterfacesTo<EnemySpawner>().AsSingle().NonLazy();      
        
        // Scene objects 
        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        Container.Bind<ISceneObjectContainer>().FromInstance(_sceneObjectContainer).AsSingle();
        
       
        
        // Providers
        Container.Bind<IEnemySpawnSettingsProvider>().To<EnemySpawnSettingsProvider>().AsSingle();
    }
}

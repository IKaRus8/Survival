using Logic.Services;
using UnityEngine;
using Zenject;

public class CommonZenjectInstaller : MonoInstaller
{  
    [SerializeField] 
    private Joystick _joystick;  
    [SerializeField] 
    private Camera _camera;
    
    public override void InstallBindings()
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
    }
}

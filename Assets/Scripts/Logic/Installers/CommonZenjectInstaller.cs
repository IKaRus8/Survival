using UnityEngine;
using Zenject;

public class CommonZenjectInstaller : MonoInstaller
{  
   
    [SerializeField] private Joystick _joystick;  
    [SerializeField] private CameraMove _cameraMove;
    public override void InstallBindings()
    {
        Container.Bind<IAssetService>().To<AssetService>().AsSingle();
        Container.Bind<ICreator<IPlayer>>().To<PlayerCreator>().AsSingle();
        Container.Bind<IPlayer>().To<Player>().AsSingle();
        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        Container.Bind<IInput>().To<MobileInput>().AsSingle();
        Container.BindInterfacesTo<PlayerHolder>().AsSingle();      
        Container.Bind<CameraMove>().FromInstance(_cameraMove).AsSingle();
        Container.Bind<GameManager>().AsSingle().NonLazy();
    }
}

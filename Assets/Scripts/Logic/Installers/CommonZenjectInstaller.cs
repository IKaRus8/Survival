using UnityEngine;
using Zenject;

public class CommonZenjectInstaller : MonoInstaller
{  
   
    [SerializeField] private Joystick _joystick;
    [SerializeField] private Controller _controller;   
    [SerializeField] private CameraMove _cameraMove;    




  
    
    public override void InstallBindings()
    {
        Container.Bind<IAssetService>().To<AssetService>().AsSingle();
        //Container.Bind<IInstatiator>().FromInstance(_instantiator).AsSingle();        
        //Container.Bind<IInstantiator>().To<DiContainer>().AsSingle();
        Container.Bind<ICreator<IPlayer>>().To<PlayerCreator>().AsSingle();
        Container.Bind<IPlayer>().To<Player>().AsSingle();
        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();          
        Container.Bind<IInput>().To<MobileInput>().AsSingle();        
        Container.Bind<IController>().FromInstance(_controller).AsSingle();   
        Container.Bind<CameraMove>().FromInstance(_cameraMove).AsSingle();
    }
}

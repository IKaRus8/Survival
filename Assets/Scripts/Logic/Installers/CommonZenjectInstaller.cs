using UnityEngine;
using Zenject;

public class CommonZenjectInstaller : MonoInstaller
{
   
    [Header("Player")] 
    [SerializeField] private Player _player;
    [SerializeField] private Transform playerSpawnPoint;


    [Space(10)]
    [Header("Input")]

    [SerializeField] private Joystick _joystick;
    [SerializeField] private Transform _joystickSpawnHook;

    [Space(10)]    
    [Header("Controller")]

    [SerializeField] private Controller _controller;

    [Space(10)]
    [Header("Camera")]
    [SerializeField] private CameraMove _cameraMove;




    
    public override void InstallBindings()
    {
        BindPlayer();
        BindInput();
        BindController();
        BindCamera();
    }


    public  void BindPlayer()
    {
        Container.Bind<IMove>().To<Movable>().AsSingle();
        var playerInstance = Container.InstantiatePrefabForComponent<Player>(_player, playerSpawnPoint.position, Quaternion.identity, null);
        Container.Bind<Player>().FromInstance(playerInstance).AsSingle();   
        
    }

   


    public void BindInput()
    {
        if (Application.isMobilePlatform)
        {
            var _joystickInstance = Container.InstantiatePrefabForComponent<Joystick>(_joystick, _joystickSpawnHook);
            Container.Bind<Joystick>().FromInstance(_joystickInstance).AsSingle();
            Container.Bind<IInput>().To<MobileInput>().AsSingle();
        }
        else
        {
            Container.Bind<IInput>().To<PCInput>().FromNew().AsSingle();
        }
    }

    public void BindController()
    {
        Container.Bind<IController>().To<Controller>().FromComponentInNewPrefab(_controller).AsSingle();
       
    }   

    public void BindCamera()
    {
        var cameraMove = Container.InstantiatePrefabForComponent<CameraMove>(_cameraMove);
        Container.Bind<CameraMove>().FromInstance(cameraMove).AsSingle();

    }





}

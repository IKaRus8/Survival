using System;
using R3;
using UnityEngine;
using Zenject;

public class CameraMoveSystem : IDisposable
{ 
    private Vector3 offset=new Vector3(0,10,-5);

    private Transform _transform;
    private Transform _playerTransform;
    private CompositeDisposable _disposables;

    public CameraMoveSystem(
        IPlayerHolder playerHolder,
        Camera camera)
    {
        _transform = camera.transform;
        _disposables = new CompositeDisposable();
        
        playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
    }   

    private void UpdateCameraPosition(Unit _)
    {
        if (_playerTransform == null)
        {
            return;
        }
        
       _transform.position = _playerTransform.position + offset;         
    }
    
    private void OnPlayerCreated(IPlayer player)
    {
        if (player == null)
        {
            return;
        }
            
        _playerTransform = player.Transform;

        Observable.EveryUpdate().Subscribe(UpdateCameraPosition).AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }
}

public class MoveSystem
{
    [Inject(Id = "move id")]
    private IMoveController _currentController;

    private void MovePlayer()
    {
        _currentController.Move();
    }

    public void SetController(IMoveController newController)
    {
        _currentController = newController;
    }
}

public class GameManagerNew : MonoBehaviour
{
    private MoveSystem _moveSystem;
    private DiContainer _container;
    
    private void Awake()
    {
        _moveSystem.SetController(new WalkMove());


        _container.Bind<IMoveController>().To<WalkMove>().AsSingle().WithConcreteId("move id");
        _container.Bind<IMoveController>().To<RideMove>().AsSingle().WithConcreteId("ride id");
    }

    public void ToCar()
    {
        _moveSystem.SetController(new RideMove());
    }
}

public interface IMoveController
{
    void Move();
}

public class WalkMove : IMoveController
{
    public void Move()
    {
        //walk
    }
}

public class RideMove : IMoveController
{
    public void Move()
    {
        //ride
    }
}

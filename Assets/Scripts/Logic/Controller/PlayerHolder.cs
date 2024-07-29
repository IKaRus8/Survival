using R3;
using System;
using UnityEngine;
using Zenject;

public class PlayerHolder :  IController, IDisposable, IMovable
{
    private IInput _input;
    private IPlayer _player;    
    private CompositeDisposable _disposable = new();
       

    public PlayerHolder(IInput input)
    {
        _input = input;        
    }

    public void SetPlayer(IPlayer player)
    {
        _player = player;       
        
        Observable.EveryUpdate().Subscribe(_ => MoveUpdate()).AddTo(_disposable);
    }

    public IPlayer GetPlayer()
    {
        return _player;
    }

    public void MoveUpdate()
    {     
        if(_player == null) return;
        _player.Move(new Vector3(_input.Dir.x, 0 , _input.Dir.y), _player.Speed, Time.deltaTime);      
    } 

    void IDisposable.Dispose()
    {
        _disposable.Dispose();
    }  
}


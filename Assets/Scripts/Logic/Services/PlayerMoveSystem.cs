using System;
using R3;
using UnityEngine;

namespace Logic.Services
{
    public class PlayerMoveSystem : IMovable, IDisposable
    {
        private readonly IInput _input;
        private readonly CompositeDisposable _disposables;
        
        private IPlayer _player;
        
        public PlayerMoveSystem(
            IPlayerHolder playerHolder,
            IInput input)
        {
            _input = input;
            _disposables = new CompositeDisposable();
            
            playerHolder.PlayerRx.Subscribe(OnPlayerCreated).AddTo(_disposables);
        }

        private void OnPlayerCreated(IPlayer player)
        {
            if (player == null)
            {
                return;
            }
            
            _player = player;
            
            Observable.EveryUpdate().Subscribe(_ => MoveUpdate()).AddTo(_disposables);
        }

        public void MoveUpdate()
        {     
            _player.Move(new Vector3(_input.Dir.x, 0 , _input.Dir.y), _player.Speed, Time.deltaTime);      
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
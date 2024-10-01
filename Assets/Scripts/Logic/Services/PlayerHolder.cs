using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using R3;

namespace Logic.Services
{
    public class PlayerHolder : IPlayerHolder, IDisposable
    {
        private readonly ICreator<IPlayer> _creator;
        private readonly CompositeDisposable _disposable;
       
        public ReactiveProperty<IPlayer> PlayerRx { get; }

        public PlayerHolder(ICreator<IPlayer> creator)
        {
            _disposable = new CompositeDisposable();
            _creator = creator;
            PlayerRx = new ReactiveProperty<IPlayer>();
        
            CreatePlayer().Forget();
        }

        private async UniTaskVoid CreatePlayer()
        {
            var player = await _creator.CreateAsync();
        
            SetPlayer(player);
        }

        private void SetPlayer(IPlayer player)
        {
            PlayerRx.Value = player;         
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }  
    }
}
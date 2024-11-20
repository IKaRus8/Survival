using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using R3;

namespace Logic.Services.Level.Hero
{
    public class HeroHolder : IHeroHolder, IDisposable
    {
        private readonly IHeroSpawner _heroSpawner;
        private readonly CompositeDisposable _disposable;
       
        public ReactiveProperty<IHero> HeroRx { get; }

        public HeroHolder(IHeroSpawner heroSpawner)
        {
            _disposable = new CompositeDisposable();
            _heroSpawner = heroSpawner;
            HeroRx = new ReactiveProperty<IHero>();
        
            CreatePlayer().Forget();
        }

        private async UniTaskVoid CreatePlayer()
        {
            var player = await _heroSpawner.CreateAsync();
        
            SetPlayer(player);
        }

        private void SetPlayer(IHero hero)
        {
            HeroRx.Value = hero;         
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }  
    }
}
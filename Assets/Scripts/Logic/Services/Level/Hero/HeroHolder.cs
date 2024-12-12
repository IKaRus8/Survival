using System;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using R3;

namespace Logic.Services.Level.Hero
{
    public class HeroHolder : IHeroHolder
    {
        private readonly IHeroSpawner _heroSpawner;
       
        public ReactiveProperty<IHero> HeroRx { get; }

        public HeroHolder(IHeroSpawner heroSpawner)
        {
            _heroSpawner = heroSpawner;
            HeroRx = new ReactiveProperty<IHero>();
        
            CreatePlayer().Forget();
        }

        private async UniTaskVoid CreatePlayer()
        {
            var player = await _heroSpawner.CreateAsync(Constants.Hero.Id.SimpleHero);
        
            SetPlayer(player);
        }

        private void SetPlayer(IHero hero)
        {
            HeroRx.Value = hero;         
        }
    }
}
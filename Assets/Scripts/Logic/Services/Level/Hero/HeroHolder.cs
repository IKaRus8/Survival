using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using R3;
using UnityEngine;

namespace Logic.Services.Level.Hero
{
    public abstract class HeroHolder : IHeroHolder
    {
        private readonly IHeroSpawner _heroSpawner;
        
        protected abstract string HeroId { get; }
       
        public ReactiveProperty<IHero> HeroRx { get; }

        public HeroHolder(IHeroSpawner heroSpawner)
        {
            _heroSpawner = heroSpawner;
            HeroRx = new ReactiveProperty<IHero>();

			CreateHeroAsync().Forget();
        }

		public async UniTaskVoid CreateHeroAsync()
        {
            var hero = await _heroSpawner.CreateAsync(HeroId);
        
            SetHero(hero);
        }

        public void HeroDie()
        {
            Object.Destroy(HeroRx.Value.Transform.gameObject, 2f);
            
            SetHero(null);
        }

        private void SetHero(IHero hero)
        {
            HeroRx.Value = hero;         
        }
    }
}
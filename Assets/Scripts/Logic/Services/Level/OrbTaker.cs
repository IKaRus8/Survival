using System;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Enums;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Player;
using Logic.RuntimeData.Rectangles;
using R3;
using Utilities.Extensions;

namespace Logic.Services.Level
{
    public class OrbTaker : IDisposable
    {
        private readonly IOrbsProvider _orbsProvider;
        private readonly IDisposable _heroDisposable;
        
        private IDisposable _tickDisposable;
        private IHero _hero;

        public OrbTaker(
            IOrbsProvider orbsProvider,
            IHeroHolder heroHolder)
        {
            _orbsProvider = orbsProvider;

            _heroDisposable = heroHolder.HeroRx.Subscribe(OnHeroCreated);
        }

        private void OnHeroCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }
            
            _hero = hero;
            
            _heroDisposable?.Dispose();
            
            _tickDisposable = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(Tick);
        }

        private void Tick(Unit _)
        {
            var heroRectangle = new Rectangle(_hero.Position, 4f);
            
            foreach (var orb in _orbsProvider.Orbs)
            {
                if (orb.IsTaken)
                {
                    continue;
                }
                
                var orbRectangle = new Rectangle(orb.Position, 1f);

                if (heroRectangle.IsIntersection(orbRectangle))
                {
                    TakeOrb(orb).Forget();
                }
            }
        }

        private async UniTask TakeOrb(IOrb orb)
        {
            await orb.FlyTo(_hero.Position);
            
            ApplyOrbEffect(orb.OrbType);
        }

        private void ApplyOrbEffect(OrbTypes orbType)
        {
            switch (orbType)
            {
                case OrbTypes.Heal:
                {
                    var healAmount = _hero.Model.Health * 0.1f;
                    
                    _hero.Heal(healAmount);
                    
                    break;
                }
                
                case OrbTypes.None: break;
                case OrbTypes.Experience: break;
                default: throw new ArgumentOutOfRangeException(nameof(orbType), orbType, null);
            }
        }

        public void Dispose()
        {
            _heroDisposable?.Dispose();
            _tickDisposable?.Dispose();
        }
    }
}
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using R3;
using Zenject;

namespace UI.View
{
    public class HeroHpBarView : HpBarView
    {
        [Inject]
        private void Construct(IHeroHolder heroHolder)
        {
            heroHolder.HeroRx.Subscribe(OnPlayerCreated).AddTo(this);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                _target = null;
                FillValue(0f);
                
                return;
            }
            
            _maxHp = hero.Model.Health;
            _target = hero;
        }
    }
}
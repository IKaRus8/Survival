using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Player;
using R3;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.View
{
    public class HpBarView : MonoBehaviour
    {
        [SerializeField, Required]
        private Image _value;

        private IHero _hero;
        private float _maxHp;

        [Inject]
        private void Construct(IHeroHolder heroHolder)
        {
            heroHolder.HeroRx.Subscribe(OnPlayerCreated).AddTo(this);
        }

        private void OnPlayerCreated(IHero hero)
        {
            if (hero == null)
            {
                return;
            }
            
            _hero = hero;

            _maxHp = _hero.Model.Health;
        }
        
        private void LateUpdate()
        {
            if (_hero == null)
            {
                return;
            }
            
            _value.fillAmount = _hero.Health / _maxHp;
        }
    }
}
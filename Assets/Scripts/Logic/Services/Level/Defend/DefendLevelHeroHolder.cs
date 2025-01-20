using Assets.Scripts.Logic.Interfaces.Services.DefendLevel;
using Assets.Scripts.Logic.Unity.DefendLevel;
using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using R3;
using Unity.Netcode;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level.Defend
{
    public class DefendLevelHeroHolder : IDefendHeroesHolder
	{
		public ReactiveProperty<IHero> HeroRx { get; private set; }

		public DefendLevelHeroHolder()
		{
			HeroRx = new ReactiveProperty<IHero>();
		}

		public void HeroDie()
		{
			GameObject.Destroy(HeroRx.Value.Transform.gameObject, 2f);

			SetHero(null);
		}

		public void SetHero(IHero hero)
		{
			HeroRx.Value = hero;
		}
	}
}
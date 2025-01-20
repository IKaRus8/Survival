
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;

namespace Assets.Scripts.Logic.Interfaces.Services.DefendLevel
{
	public interface IDefendHeroesHolder : IHeroHolder
	{
		void SetHero(IHero hero);
	}
}
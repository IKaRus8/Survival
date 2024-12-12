using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Player;
using R3;

namespace Logic.Interfaces.Services.Player
{
    public interface  IHeroHolder 
    {
        ReactiveProperty<IHero> HeroRx { get; }
    }
}

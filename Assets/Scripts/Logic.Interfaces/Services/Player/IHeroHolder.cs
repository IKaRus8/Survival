using Logic.Interfaces.Unity;
using R3;

namespace Logic.Interfaces.Services.Player
{
    public interface  IHeroHolder 
    {
        ReactiveProperty<IHero> HeroRx { get; }
    }
}

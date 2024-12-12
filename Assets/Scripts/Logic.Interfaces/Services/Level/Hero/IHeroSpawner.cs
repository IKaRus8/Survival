using Cysharp.Threading.Tasks;
using Logic.Interfaces.Unity.Player;

namespace Logic.Interfaces.Services.Level.Hero
{
    public interface IHeroSpawner
    {
        UniTask<IHero> CreateAsync(string heroId);
    }
}

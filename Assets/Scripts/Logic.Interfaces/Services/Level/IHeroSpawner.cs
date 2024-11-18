using Cysharp.Threading.Tasks;
using Logic.Interfaces.Unity;

namespace Logic.Interfaces.Services.Level
{
    public interface IHeroSpawner
    {
        UniTask<IHero> CreateAsync();
    }
}

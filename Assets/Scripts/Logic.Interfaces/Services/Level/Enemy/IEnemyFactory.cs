using Cysharp.Threading.Tasks;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;

namespace Logic.Interfaces.Services.Level.Enemy
{
    public interface IEnemyFactory
    {
        UniTask<IEnemy> CreateAsync(string id);
    }
}
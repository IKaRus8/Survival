using Cysharp.Threading.Tasks;
using Logic.RuntimeData;

namespace Logic.Interfaces.Services.Level
{
    public interface ILevelLoader
    {
        UniTask LoadLevel();
        UniTask LoadLevel(LevelParameters parameters);
    }
}
using Cysharp.Threading.Tasks;

namespace Logic.Interfaces.Services
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(string sceneName);

        UniTask LoadMenuSceneAsync();

        UniTask LoadSurvivalLevelSceneAsync();

        UniTask LoadDefendLevelSceneAsync();
    }
}
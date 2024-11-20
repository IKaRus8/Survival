using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Logic.Interfaces.Services;
using UnityEngine.AddressableAssets;

namespace Logic.Services
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask LoadSceneAsync(string sceneName)
        {
            await Addressables.LoadSceneAsync(sceneName);
        }

        public async UniTask LoadMenuSceneAsync()
        {
            await LoadSceneAsync(Constants.Scenes.MenuScene);
        }

        public async UniTask LoadLevelSceneAsync()
        {
            await LoadSceneAsync(Constants.Scenes.LevelScene);
        }
    }
}
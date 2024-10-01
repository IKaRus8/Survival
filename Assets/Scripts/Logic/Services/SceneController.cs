using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Logic.Services
{
    public class SceneLoader : ISceneLoader
    {
        //TODO: добавить остальные сцены
        private const string MapSceneKey = "MainMenu";

        public async UniTask LoadSceneAsync(string sceneName)
        {
            await Addressables.LoadSceneAsync(sceneName);
        }

        public async UniTask LoadMapSceneAsync()
        {
            await Addressables.LoadSceneAsync(MapSceneKey);
        }
    }

//TODO: отдельный файл
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(string sceneName);

        UniTask LoadMapSceneAsync();
    }
}
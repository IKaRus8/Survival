using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class SceneLoader: ISceneLoader
{ 
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

public interface ISceneLoader
{
    UniTask LoadSceneAsync(string sceneName);

    UniTask LoadMapSceneAsync();
}

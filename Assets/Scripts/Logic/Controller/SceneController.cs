using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

public class SceneController: ISceneController
{ 
    public async UniTask LoadSceneAsync(string sceneName)
    {
        await Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single, true);
    }
}

public interface ISceneController
{
    UniTask LoadSceneAsync(string sceneName);
}

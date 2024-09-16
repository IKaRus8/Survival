using Cysharp.Threading.Tasks;
using UnityEngine;

public class ServicesLoader : IServicesLoader
{
    private const string sceneName = "MainMenu";
    private ISceneController _sceneController;
    public ServicesLoader(ISceneController sceneController)
    {
        _sceneController = sceneController;
        LoadingServices();
    }

    public void LoadingServices()
    {
        LoadServices();
    }
    public async UniTaskVoid LoadServices()
    {
        //что-то там грузим
        //грузим первую сцену
        Debug.Log("Loading services");
        await UniTask.Delay(3000);
        Debug.Log("Dropping scene");
        await _sceneController.LoadSceneAsync(sceneName);
    }
}

public interface IServicesLoader
{
    UniTaskVoid LoadServices(); 
}

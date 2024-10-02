using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic.Services
{
    public class GameBootstraper : IGameBootstraper
    {
        private const string sceneName = "MainMenu";
    
        private ISceneLoader _sceneLoader;
    
        public GameBootstraper(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        
            LoadServices().Forget();
        }
    
        public async UniTaskVoid LoadServices()
        {
            //что-то там грузим
            //грузим первую сцену
            Debug.Log("Loading services");
            await UniTask.Delay(3000);
        
            Debug.Log("Dropping scene");
            await _sceneLoader.LoadSceneAsync(sceneName);
        }
    }

    public interface IGameBootstraper
    {
        UniTaskVoid LoadServices(); 
    }
}
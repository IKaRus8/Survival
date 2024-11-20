using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;

namespace Logic.Services
{
    public class GameBootstrapper
    {
        private readonly ISceneLoader _sceneLoader;
    
        public GameBootstrapper(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        
            LoadServices().Forget();
        }

        private async UniTaskVoid LoadServices()
        {
            await _sceneLoader.LoadMenuSceneAsync();
        }
    }
}
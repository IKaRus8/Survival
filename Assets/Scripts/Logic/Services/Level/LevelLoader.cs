using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.RuntimeData;

namespace Logic.Services.Level
{
    public class LevelLoader : ILevelLoader, ILevelParametersHolder
    {
        private readonly ISceneLoader _sceneLoader;
        
        public LevelParameters Parameters { get; private set; }

        public LevelLoader(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask LoadLevel()
        {
            var parameters = new LevelParameters();

            parameters.SetAsTimeLevel();

            await LoadLevel(parameters);
        }

        public async UniTask LoadLevel(LevelParameters parameters)
        {
            Parameters = parameters;
            
            await _sceneLoader.LoadSurvivalLevelSceneAsync();
        }
    }
}
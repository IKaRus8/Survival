using Logic.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

//to play button
namespace UI
{
    public class UIWindow : MonoBehaviour
    {
        private const string GameSceneName = "Survivour";
        [SerializeField]
        private Button _startGameButton;

        private ISceneLoader _sceneLoader;


        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Awake()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _startGameButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _startGameButton.onClick.RemoveAllListeners();

            _sceneLoader.LoadSceneAsync(GameSceneName);
        }
    }
}
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons
{
    public class DefendPlayButton : MonoBehaviour
    {
        private ISceneLoader _sceneLoader;
        private Button _button;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _button.interactable = false;

            _sceneLoader.LoadDefendLevelSceneAsync().Forget();
        }
    }
}
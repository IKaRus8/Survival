using Logic.Services;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Popups
{
    public class GameEndedPopup : MonoBehaviour
    {
        [SerializeField]
        private Button _menuButton;

        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Awake()
        {
            _menuButton.onClick.AddListener(BackToMenu);
        }

        private void BackToMenu()
        {
            _sceneLoader.LoadMapSceneAsync();
        }
    }
}
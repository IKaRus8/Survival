using Logic.Interfaces.Services;
using UI.Interfaces.Popups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Popups
{
    public class GameEndedPopup : PopupBase, IGameEndedPopup
    {
        [SerializeField]
        private Button _menuButton;

        private ISceneLoader _sceneLoader;

        public override string Key => "GameEndedPopup";

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
            _sceneLoader.LoadMenuSceneAsync();
        }
    }
}
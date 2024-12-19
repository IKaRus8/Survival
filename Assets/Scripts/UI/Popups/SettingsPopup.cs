using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;
using Sirenix.OdinInspector;
using UI.Interfaces.Popups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Popups
{
    public class SettingsPopup : PopupBase, ISettingsPopup
    {
        [SerializeField, Required]
        private Button _resumeButton;
        
        [SerializeField, Required]
        private Button _menuButton;

        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }
        
        private void Awake()
        {
            _resumeButton.onClick.AddListener(Resume);
            _menuButton.onClick.AddListener(ToMenu);
        }

        private void Resume()
        {
            Close();
        }

        private void ToMenu()
        {
            _sceneLoader.LoadMenuSceneAsync().Forget();
        }
    }
}
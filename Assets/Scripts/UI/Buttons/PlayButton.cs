using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class PlayButton : MonoBehaviour
    {
        private Button _button;
        private ILevelLoader _levelLoader;

        [Inject]
        public void Construct(ILevelLoader levelLoader)
        {
            _levelLoader = levelLoader;
        }

        public void Awake()
        {
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _button.interactable = false;

            _levelLoader.LoadLevel();
        }
    }
}
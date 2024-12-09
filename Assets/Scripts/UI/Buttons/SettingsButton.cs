using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class SettingsButton : MonoBehaviour
    {
        [Inject]
        private readonly ISettingsPopupPresenter _popupPresenter;

        private void Awake()
        {
            var button = GetComponent<Button>();
            
            button.onClick.AddListener(ShowSettingsPopup);
        }

        private void ShowSettingsPopup()
        {
            _popupPresenter.Show().Forget();
        }
    }
}
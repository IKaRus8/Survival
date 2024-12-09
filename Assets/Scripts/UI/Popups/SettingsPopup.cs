using Sirenix.OdinInspector;
using UI.Interfaces.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class SettingsPopup : PopupBase, ISettingsPopup
    {
        [SerializeField, Required]
        private Button _resumeButton;

        private void Awake()
        {
            _resumeButton.onClick.AddListener(Resume);
        }

        private void Resume()
        {
            Close();
        }
    }
}
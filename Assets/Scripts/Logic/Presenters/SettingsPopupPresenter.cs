using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using UI.Interfaces.Popups;

namespace Logic.Presenters
{
    public class SettingsPopupPresenter : ISettingsPopupPresenter
    {
        private const string SettingsPopupKey = "SettingsPopup";

        private readonly IPauseService _pauseService;
        private readonly IPopupSystem _popupSystem;

        public SettingsPopupPresenter(
            IPauseService pauseService,
            IPopupSystem popupSystem)
        {
            _pauseService = pauseService;
            _popupSystem = popupSystem;
        }
        
        public async UniTask Show()
        {
            _pauseService.Pause();
            
            var popup = await _popupSystem.ShowPopup<ISettingsPopup>(SettingsPopupKey);

            popup.OnClose += Resume;
        }

        private void Resume()
        {
            _pauseService.Resume();
        }
    }
}
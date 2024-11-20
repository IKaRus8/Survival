using Cysharp.Threading.Tasks;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services;
using UI.Interfaces.Popups;

namespace Logic.Presenters
{
    public class GameEndedPopupPresenter : IGameEndedPopupPresenter
    {
        private const string GameEndedPopupKey = "GameEndedPopup";
        
        private readonly IPopupSystem _popupSystem;

        public GameEndedPopupPresenter(IPopupSystem popupSystem)
        {
            _popupSystem = popupSystem;
        }
        
        public async UniTask ShowPopup()
        {
            await _popupSystem.ShowPopup<IGameEndedPopup>(GameEndedPopupKey);
        }
    }
}
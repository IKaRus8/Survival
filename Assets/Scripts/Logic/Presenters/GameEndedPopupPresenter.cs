using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Presenters;
using Logic.Interfaces.Services;
using Logic.Popups;
using UnityEngine;
using Zenject;

namespace Logic.Presenters
{
    public class GameEndedPopupPresenter : IGameEndedPopupPresenter
    {
        private const string GameEndedPopupKey = "GameEndedPopup";
        
        private readonly ILevelSceneObjectContainer _levelSceneObjectContainer;
        private readonly IAssetService _assetService;
        private readonly IInstantiator _instantiator;

        private GameEndedPopup _popup;

        public GameEndedPopupPresenter(
            ILevelSceneObjectContainer levelSceneObjectContainer,
            IAssetService assetService,
            IInstantiator instantiator)
        {
            _levelSceneObjectContainer = levelSceneObjectContainer;
            _assetService = assetService;
            _instantiator = instantiator;
        }
        
        public async UniTask ShowPopup()
        {
            if (_popup != null)
            {
                _popup.gameObject.SetActive(true);
                return;
            }
            
            var popupPrefab = await _assetService.LoadAssetAsync<GameObject>(GameEndedPopupKey);

            _popup = _instantiator.InstantiatePrefabForComponent<GameEndedPopup>(popupPrefab, _levelSceneObjectContainer.PopupContainer);
        }
    }
}
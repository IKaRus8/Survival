using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Presenters;
using Logic.Popups;
using UnityEngine;
using Zenject;

namespace Logic.Presenters
{
    public class GameEndedPopupPresenter : IGameEndedPopupPresenter
    {
        private const string GameEndedPopupKey = "GameEndedPopup";
        
        private readonly ISceneObjectContainer _sceneObjectContainer;
        private readonly IAssetService _assetService;
        private readonly IInstantiator _instantiator;

        private GameEndedPopup _popup;

        public GameEndedPopupPresenter(
            ISceneObjectContainer sceneObjectContainer,
            IAssetService assetService,
            IInstantiator instantiator)
        {
            _sceneObjectContainer = sceneObjectContainer;
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
            
            var popupPrefab = await _assetService.GetAssetAsync<GameObject>(GameEndedPopupKey);

            _popup = _instantiator.InstantiatePrefabForComponent<GameEndedPopup>(popupPrefab, _sceneObjectContainer.PopupContainer);
        }
    }
}
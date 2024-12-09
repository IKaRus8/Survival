using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;
using Logic.Unity.SceneObjects;
using UnityEngine;
using Zenject;

namespace Logic.Services
{
    public class PopupSystem : IPopupSystem
    {
        private readonly IAssetService _assetService;
        private readonly IInstantiator _instantiator;

        public PopupSystem(
            IAssetService assetService,
            IInstantiator instantiator)
        {
            _assetService = assetService;
            _instantiator = instantiator;
        }
        
        public async UniTask<T> ShowPopup<T>(string key)
        {
            var popupPrefab = await _assetService.LoadAssetAsync<GameObject>(key);

            var popup = _instantiator.InstantiatePrefabForComponent<T>(popupPrefab, BaseSceneObjectsContainer.PopupContainer);

            return popup;
        }
    }
}
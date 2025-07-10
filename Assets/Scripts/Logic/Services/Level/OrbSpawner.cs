using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Unity;
using UnityEngine;

namespace Logic.Services.Level
{
    public class OrbSpawner : IOrbSpawner
    {
        private readonly IAssetService _assetService;
        private readonly ILevelSceneObjectContainer _levelSceneObjectContainer;
        private readonly IOrbsProvider _orbsProvider;

        public OrbSpawner(
            IAssetService assetService,
            ILevelSceneObjectContainer levelSceneObjectContainer,
            IOrbsProvider orbsProvider)
        {
            _assetService = assetService;
            _levelSceneObjectContainer = levelSceneObjectContainer;
            _orbsProvider = orbsProvider;
        }
        
        public async UniTask SpawnOrb(string orbId, Vector3 position)
        {
            var orb = await _assetService
                .LoadAndInstantiateAsync<IOrb>(orbId, _levelSceneObjectContainer.OrbsContainer);
            
            orb.MoveTo(position);
            
            _orbsProvider.AddOrb(orb);
        }
    }
}
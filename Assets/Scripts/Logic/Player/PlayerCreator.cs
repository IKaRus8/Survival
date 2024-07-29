
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class PlayerCreator: ICreator<IPlayer>
{
   private IAssetService _assetService;  
   private IInstantiator _container;

   public PlayerCreator(IAssetService  assetServise, IInstantiator diContainer)
   {
        _assetService = assetServise;
        _container = diContainer;
   }

   public async UniTask<IPlayer> CreateAsync()
   {
        var playerGameObject = await _assetService.GetAssetAsync<GameObject>("Assets/Prefabs/Game/Player.prefab");      
      
        var player = _container.InstantiatePrefabForComponent<IPlayer>(playerGameObject);
       
        return player;
    }
}

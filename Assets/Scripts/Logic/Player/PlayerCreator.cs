using System.Threading.Tasks;
using UnityEngine;

public class PlayerCreator: ICreator<IPlayer>
{
   private IAssetService _assetService;
   private IInstatiator _instatiator;

   public PlayerCreator(IAssetService  assetServise, IInstatiator instatiator)
   {
        _assetService = assetServise;
        _instatiator = instatiator;
   }

   public async Task<IPlayer> CreateAsync()
   {
        var playerGameObject = await _assetService.GetAssetAsync<GameObject>("Assets/Prefabs/Game/Player.prefab");
       
        var playerPrefab = playerGameObject.GetComponent<IPlayer>();

        _instatiator.InstantiateAsset(playerGameObject);
        return playerPrefab;
    }
}

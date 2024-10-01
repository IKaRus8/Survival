using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using UnityEngine;
using Zenject;

namespace Logic.Services
{
     public class PlayerCreator : ICreator<IPlayer>
     {
          private const string PlayerKey = "Player";

          private readonly IAssetService _assetService;
          private readonly IInstantiator _container;

          public PlayerCreator(
               IAssetService assetService,
               IInstantiator diContainer)
          {
               _assetService = assetService;
               _container = diContainer;
          }

          public async UniTask<IPlayer> CreateAsync()
          {
               var playerGameObject = await _assetService.GetAssetAsync<GameObject>(PlayerKey);

               var player = _container.InstantiatePrefabForComponent<IPlayer>(playerGameObject);

               return player;
          }
     }
}
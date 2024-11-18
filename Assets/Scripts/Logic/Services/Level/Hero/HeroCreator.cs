using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Unity;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level.Hero
{
     public class HeroCreator : IHeroSpawner
     {
          private const string PlayerKey = "Player";

          private readonly IAssetService _assetService;
          private readonly IInstantiator _container;

          public HeroCreator(
               IAssetService assetService,
               IInstantiator diContainer)
          {
               _assetService = assetService;
               _container = diContainer;
          }

          public async UniTask<IHero> CreateAsync()
          {
               var playerGameObject = await _assetService.LoadAssetAsync<GameObject>(PlayerKey);

               var player = _container.InstantiatePrefabForComponent<IHero>(playerGameObject);

               return player;
          }
     }
}
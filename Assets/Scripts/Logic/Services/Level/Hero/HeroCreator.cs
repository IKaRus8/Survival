using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Logic.Interfaces.Providers.Level.Hero;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Unity;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level.Hero
{
     public class HeroCreator : IHeroSpawner
     {
          private const string PlayerKey = "player";
          private const string HeroId = Constants.Hero.Id.SimpleHero;

          private readonly IAssetService _assetService;
          private readonly IHeroModelsProvider _heroModelsProvider;
          private readonly IInstantiator _container;
          private readonly Transform _levelContainer;

          public HeroCreator(
               IAssetService assetService,
               IHeroModelsProvider heroModelsProvider,
               ILevelSceneObjectContainer sceneObjectContainer,
               IInstantiator diContainer)
          {
               _assetService = assetService;
               _heroModelsProvider = heroModelsProvider;
               _container = diContainer;
               
               _levelContainer = sceneObjectContainer.LevelContainer;
          }

          public async UniTask<IHero> CreateAsync()
          {
               var heroGameObject = await _assetService.LoadAssetAsync<GameObject>(PlayerKey);

               var hero = _container.InstantiatePrefabForComponent<IHero>(heroGameObject, _levelContainer);
               
               var heroModel = _heroModelsProvider.GetHeroModel(HeroId);
               
               hero.Initialize(heroModel);

               return hero;
          }
     }
}
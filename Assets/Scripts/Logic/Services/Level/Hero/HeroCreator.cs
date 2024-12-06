using Cysharp.Threading.Tasks;
using Data.Interfaces.Constants;
using Logic.Interfaces.Providers.Level;
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
          private const string HeroId = Constants.Hero.Id.SimpleHero;

          private readonly IAssetService _assetService;
          private readonly IHeroModelsProvider _heroModelsProvider;
          private readonly IAttackModelsProvider _attackModelsProvider;
          private readonly IInstantiator _container;
          private readonly Transform _levelContainer;

          public HeroCreator(
               IAssetService assetService,
               IHeroModelsProvider heroModelsProvider,
               ILevelSceneObjectContainer sceneObjectContainer,
               IAttackModelsProvider attackModelsProvider,
               IInstantiator diContainer)
          {
               _assetService = assetService;
               _heroModelsProvider = heroModelsProvider;
               _attackModelsProvider = attackModelsProvider;
               _container = diContainer;
               
               _levelContainer = sceneObjectContainer.LevelContainer;
          }

          public async UniTask<IHero> CreateAsync()
          {
               var heroGameObject = await _assetService.LoadAssetAsync<GameObject>(HeroId);

               var hero = _container.InstantiatePrefabForComponent<IHero>(heroGameObject, _levelContainer);
               
               var heroModel = _heroModelsProvider.GetHeroModel(HeroId);
               var attackModel = _attackModelsProvider.GetAttackModel(heroModel.AttackModelId);
               
               hero.Initialize(heroModel, attackModel);

               return hero;
          }
     }
}
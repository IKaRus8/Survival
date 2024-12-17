using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Hero;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Unity.Player;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level.Hero
{
     public class HeroCreator : IHeroSpawner
     {
          private readonly IAssetService _assetService;
          private readonly IHeroModelsProvider _heroModelsProvider;
          private readonly IBaseSceneObjectContainer _sceneObjectContainer;
          private readonly IAttackModelsProvider _attackModelsProvider;
          private readonly IInstantiator _container;

          public HeroCreator(
               IAssetService assetService,
               IHeroModelsProvider heroModelsProvider,
               IBaseSceneObjectContainer sceneObjectContainer,
               IAttackModelsProvider attackModelsProvider,
               IInstantiator diContainer)
          {
               _assetService = assetService;
               _heroModelsProvider = heroModelsProvider;
               _sceneObjectContainer = sceneObjectContainer;
               _attackModelsProvider = attackModelsProvider;
               _container = diContainer;
          }

          public async UniTask<IHero> CreateAsync(string heroId)
          {
               var heroGameObject = await _assetService.LoadAssetAsync<GameObject>(heroId);
               var position = RandomHelper.GetRandomVector(6f);

               var hero = _container.InstantiatePrefabForComponent<IHero>(
                    heroGameObject,
                    position,
                    quaternion.identity, 
                    _sceneObjectContainer.LevelContainer);
               
               var heroModel = _heroModelsProvider.GetHeroModel(heroId);
               var attackModel = _attackModelsProvider.GetAttackModel(heroModel.AttackModelId);
               
               hero.Initialize(heroModel, attackModel);

               return hero;
          }
     }
}
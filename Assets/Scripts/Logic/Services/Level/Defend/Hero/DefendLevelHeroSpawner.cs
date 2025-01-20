using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Hero;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Unity.Player;
using Logic.Services;
using Logic.Services.Level.Hero;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Logic.Services.Level.Defend.Hero
{
	public class DefendLevelHeroSpawner : IHeroSpawner
	{
		private readonly IAssetService _assetService;
		private readonly IHeroModelsProvider _heroModelsProvider;
		private readonly IBaseSceneObjectContainer _sceneObjectContainer;
		private readonly IAttackModelsProvider _attackModelsProvider;
		private readonly IInstantiator _container;

		public DefendLevelHeroSpawner(
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

			var hero = GameObject.Instantiate(heroGameObject);// _container.InstantiatePrefab(heroGameObject);

			var networkObject = hero.GetComponent<NetworkObject>();

			networkObject.Spawn(true);

			var heroComponent = hero.GetComponent<IHero>();

			InitHero(heroComponent);

			return heroComponent;
		}

		private void InitHero(IHero hero)
		{
			var position = RandomHelper.GetRandomVector(6f);

			var heroModel = _heroModelsProvider.GetHeroModel(hero.Id);
			var attackModel = _attackModelsProvider.GetAttackModel(heroModel.AttackModelId);

			hero.Initialize(heroModel, attackModel);
		}
	}
}
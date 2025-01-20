using Cysharp.Threading.Tasks;
using Logic.Interfaces.Providers.Level.Hero;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services;
using Logic.Interfaces.Unity.Player;
using Logic.Services;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using Zenject;
using Data.Interfaces.Constants;
using Assets.Scripts.Logic.Interfaces.Services.DefendLevel;

namespace Assets.Scripts.Logic.Unity.DefendLevel
{
	public class NetworkHeroesSpawner : NetworkBehaviour
	{
		[SerializeField]
		private GameObject HeroPrefab;

		private IHeroModelsProvider _heroModelsProvider;
		private IBaseSceneObjectContainer _sceneObjectContainer;
		private IAttackModelsProvider _attackModelsProvider;
		private IInstantiator _container;
		private IDefendHeroesHolder _defendHeroesHolder;

		[Inject]
		private void Construct(
			 IHeroModelsProvider heroModelsProvider,
			 IBaseSceneObjectContainer sceneObjectContainer,
			 IAttackModelsProvider attackModelsProvider,
			 IInstantiator diContainer,
			 IDefendHeroesHolder defendHeroesHolder)
		{
			_heroModelsProvider = heroModelsProvider;
			_sceneObjectContainer = sceneObjectContainer;
			_attackModelsProvider = attackModelsProvider;
			_container = diContainer;
			_defendHeroesHolder = defendHeroesHolder;
		}

		public override void OnNetworkSpawn()
		{
			if (IsServer)
			{
				SpawnHero();
			}
		}

		public IHero SpawnHero()
		{
			var hero = GameObject.Instantiate(HeroPrefab);// _container.InstantiatePrefab(heroGameObject);

			var networkObject = hero.GetComponent<NetworkObject>();

			networkObject.Spawn(true);

			var heroComponent = hero.GetComponent<IHero>();

			InitHero(heroComponent);

			return heroComponent;
		}

		private void InitHero(IHero hero)
		{
			var position = RandomHelper.GetRandomVector(6f);

			var heroModel = _heroModelsProvider.GetHeroModel(Constants.Hero.Id.DefendHero);
			var attackModel = _attackModelsProvider.GetAttackModel(heroModel.AttackModelId);

			hero.Initialize(heroModel, attackModel);

			_defendHeroesHolder.SetHero(hero);
		}
	}
}
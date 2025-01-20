using Assets.Scripts.Logic.Interfaces.Services.DefendLevel;
using Assets.Scripts.Logic.Services.Level.Defend.Hero;
using Assets.Scripts.Logic.Unity.DefendLevel;
using Data.Interfaces.Constants;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.DefendLevel;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using Logic.Providers.Level;
using Logic.Providers.Level.Enemies;
using Logic.Services.Level.Defend;
using Logic.Services.Level.Hero;
using Logic.Services.Level.Pools;
using Logic.Unity.Projectiles;
using Logic.Unity.SceneObjects;
using UnityEngine;
using Zenject;

namespace Logic.Installers
{
    public class DefendLevelInstaller : MonoInstaller<DefendLevelInstaller>
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private GameObject _bulletPrefab;
        [SerializeField]
        private Transform _bulletsParent;

        public override void InstallBindings()
        {
            // Scene objects 
            Container.Bind<Camera>().FromInstance(_camera).AsSingle();
            Container.BindInterfacesTo<DefendLevelSceneObjectsContainer>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IDefendObject>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IEnemySpawnPoints>().FromComponentInHierarchy().AsSingle();
			Container.Bind<NetworkHeroesSpawner>().FromComponentInHierarchy().AsSingle();

			// Services
			Container.Bind<IHeroSpawner>().To<DefendLevelHeroSpawner>().AsTransient();
            Container.BindInterfacesTo<DefendEnemyStatesObserver>().AsSingle().NonLazy();
            //Container.BindInterfacesTo<DefendEnemySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DefendHeroDeathObserver>().AsSingle().NonLazy();
            Container.Bind<IEnemySpawnSettingsProvider>().To<EnemySpawnSettingsProvider>().AsSingle()
                .WithArguments(Constants.Settings.Spawn.DefendSpawnSettings);
            Container.BindInterfacesTo<DefendObjectDeathObserver>().AsSingle().NonLazy();
            Container.Bind<IDefendHeroesHolder>().To<DefendLevelHeroHolder>().AsSingle();

            // Providers
            Container.Bind<IEnemySpawnPointsProvider>().To<EnemySpawnPointsProvider>().AsSingle();
            Container.BindInterfacesTo<RectanglesProvider>().AsSingle();

            // Pools
            Container.BindMemoryPool<Projectile, ProjectilesPool>()
                .WithInitialSize(10) // Начальный размер пула
                .FromComponentInNewPrefab(_bulletPrefab) // Префаб пули
                .UnderTransform(_bulletsParent);

            //Presenters
            
            CommonLevelInstaller.Install(Container);
        }
    }
}
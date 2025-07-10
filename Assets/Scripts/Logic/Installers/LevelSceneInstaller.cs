using Data.Interfaces.Constants;
using Logic.Interfaces.Providers.Level;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Hero;
using Logic.Interfaces.Services.Player;
using Logic.Providers.Level;
using Logic.Providers.Level.Enemies;
using Logic.Services.Level;
using Logic.Services.Level.Enemy;
using Logic.Services.Level.Hero;
using Logic.Services.Level.Pools;
using Logic.Services.Level.Survival.Enemy;
using Logic.Services.Level.Survival.Grid;
using Logic.Services.Level.Survival.Hero;
using Logic.Unity.Projectiles;
using Logic.Unity.SceneObjects;
using UI.Interfaces.View;
using UI.View;
using UnityEngine;
using Zenject;

namespace Logic.Installers
{
    public class LevelSceneInstaller : MonoInstaller<LevelSceneInstaller>
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
            Container.BindInterfacesTo<LevelSceneObjectsContainer>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<ILevelTimerView>().To<LevelTimerView>().FromComponentInHierarchy().AsSingle();

			// Services
			Container.Bind<IHeroSpawner>().To<HeroSpawner>().AsTransient();
			Container.Bind<IHeroHolder>().To<SurvivalLevelHeroHolder>().AsSingle();
            Container.BindInterfacesTo<GridSystem>().AsSingle();
            Container.BindInterfacesTo<SurvivalLevelEnemySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SurvivalLevelHeroDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyStatesObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroDetectedService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyCollisionSystem>().AsSingle().NonLazy();
            Container.Bind<IEnemySpawnSettingsProvider>().To<EnemySpawnSettingsProvider>().AsSingle()
                .WithArguments(Constants.Settings.Spawn.LevelSpawnSettings);
            Container.Bind<LevelTimer>().AsSingle().NonLazy();
            Container.Bind<IGameOverService>().To<GameOverService>().AsTransient();
            Container.BindInterfacesTo<OrbSystem>().AsSingle().NonLazy();
            Container.Bind<IOrbSpawner>().To<OrbSpawner>().AsTransient();
            Container.BindInterfacesTo<OrbTaker>().AsSingle().NonLazy();
            
            // Providers
            Container.BindInterfacesTo<SurvivalLevelRectanglesProvider>().AsSingle();
            Container.Bind<IOrbSpawnConfigProvider>().To<OrbSpawnConfigProvider>().AsTransient();
            Container.Bind<IOrbsProvider>().To<OrbsProvider>().AsSingle();
            
            
            // Pools
            Container.BindMemoryPool<Projectile, ProjectilesPool>()
                .WithInitialSize(10) // Начальный размер пула
                .FromComponentInNewPrefab(_bulletPrefab) // Префаб пули
                .UnderTransform(_bulletsParent);
            
            CommonLevelInstaller.Install(Container);
        }
    }
}
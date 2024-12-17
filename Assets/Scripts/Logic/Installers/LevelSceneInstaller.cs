using Data.Interfaces.Constants;
using Logic.Interfaces.Providers.Level.Enemies;
using Logic.Interfaces.Services.Player;
using Logic.Providers.Level;
using Logic.Providers.Level.Enemies;
using Logic.Services.Level.Enemy;
using Logic.Services.Level.Grid;
using Logic.Services.Level.Hero;
using Logic.Services.Level.Pools;
using Logic.Unity.Projectiles;
using Logic.Unity.SceneObjects;
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
            
            // Services
            Container.Bind<IHeroHolder>().To<SurvivalLevelHeroHolder>().AsSingle();
            Container.BindInterfacesTo<GridSystem>().AsSingle();
            Container.BindInterfacesTo<LevelEnemySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelHeroDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyStatesObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroDetectedService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyCollisionSystem>().AsSingle().NonLazy();
            Container.Bind<IEnemySpawnSettingsProvider>().To<EnemySpawnSettingsProvider>().AsSingle()
                .WithArguments(Constants.Settings.Spawn.LevelSpawnSettings);
            
            // Providers
            Container.BindInterfacesTo<SurvivalLevelRectanglesProvider>().AsSingle();
            
            
            // Pools
            Container.BindMemoryPool<Projectile, ProjectilesPool>()
                .WithInitialSize(10) // Начальный размер пула
                .FromComponentInNewPrefab(_bulletPrefab) // Префаб пули
                .UnderTransform(_bulletsParent);
            
            CommonLevelInstaller.Install(Container);
        }
    }
}
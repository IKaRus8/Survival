using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Providers.Level;
using Logic.Services.Level.Enemy;
using Logic.Services.Level.Grid;
using Logic.Services.Level.Hero;
using Logic.Services.Level.Pools;
using Logic.Services.Level.Projectiles;
using Logic.Unity.Projectiles;
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
            Container.BindInterfacesTo<ILevelSceneObjectContainer>().FromComponentInHierarchy().AsSingle();
            
            // Services
            Container.Bind<IHeroHolder>().To<HeroHolder>().AsSingle();
            Container.BindInterfacesTo<GridSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelEnemySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<LevelHeroDeathObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyStatesObserver>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HeroDetectedService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EnemyCollisionSystem>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ProjectilesCollisionSystem>().AsSingle().NonLazy();
            
            // Providers
            Container.BindInterfacesTo<RectanglesProvider>().AsSingle();
            
            
            // Pools
            Container.BindMemoryPool<Projectile, ProjectilesPool>()
                .WithInitialSize(10) // Начальный размер пула
                .FromComponentInNewPrefab(_bulletPrefab) // Префаб пули
                .UnderTransform(_bulletsParent);
            
            CommonLevelInstaller.Install(Container);
        }
    }
}
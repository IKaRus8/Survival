using Logic.Interfaces.Services.DefendLevel;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Player;
using Logic.Interfaces.Unity.Player;
using Logic.Services.DefendLevel;
using Logic.Services.Level.Hero;
using Logic.Services.Level.Pools;
using Logic.Unity.Projectiles;
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
            Container.BindInterfacesTo<IDefendLevelSceneObjectContainer>()
                .FromComponentInHierarchy().AsSingle();
            Container.Bind<IDefendObject>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IEnemySpawnPoints>().FromComponentInHierarchy().AsSingle();

            // Services
            Container.BindInterfacesTo<NavEnemyStatesObserver>().AsSingle().NonLazy();
            Container.Bind<IHeroHolder>().To<HeroHolder>().AsSingle();
            Container.BindInterfacesTo<DefendEnemySpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<DefendHeroDeathObserver>().AsSingle().NonLazy();

            // Providers
            Container.Bind<IEnemySpawnPointsProvider>().To<EnemySpawnPointsProvider>().AsSingle();

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
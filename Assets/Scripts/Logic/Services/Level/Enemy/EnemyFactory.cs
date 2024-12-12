using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Enemy;
using Logic.Interfaces.Unity;
using Logic.Interfaces.Unity.Enemy;
using UnityEngine;
using Zenject;

namespace Logic.Services.Level.Enemy
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _container;
        private readonly IAssetService _assetService;
        private readonly Transform _enemiesContainer;

        public EnemyFactory(
            DiContainer container,
            IAssetService assetService,
            IBaseSceneObjectContainer sceneObjectContainer)
        {
            _container = container;
            _assetService = assetService;
            
            _enemiesContainer = sceneObjectContainer.EnemiesContainer;
        }

        public async UniTask<IEnemy> CreateAsync(string id)
        {
            var enemyPrefab = await _assetService
                .LoadAndInstantiateAsync(id, _enemiesContainer);

            _container.InjectGameObject(enemyPrefab);
        
            return enemyPrefab.GetComponent<IEnemy>();
        }
    }
}
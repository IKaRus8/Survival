using Logic.Interfaces.Services.DefendLevel;
using UnityEngine;

namespace Logic.Unity.SceneObjects
{
    public class EnemySpawnPoints : MonoBehaviour, IEnemySpawnPoints
    {
        [SerializeField]
        private Transform[] _spawnPoints;
        
        public Transform[] SpawnPoints => _spawnPoints;
    }
}
using System.Collections.Generic;
using Data.Models.Enemy;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(menuName = "GameSettings/Enemy", fileName = "EnemySpawnSettings")]
    public class EnemySpawnSettings : ScriptableObject
    {
        [SerializeField]
        private List<EnemySpawnParameter> _spawnParameters;
        
        public List<EnemySpawnParameter> SpawnParameters => _spawnParameters;
    }
}
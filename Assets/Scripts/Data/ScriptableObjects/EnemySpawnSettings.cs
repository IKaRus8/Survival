using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ScriptableObjects
{
    [CreateAssetMenu(menuName = "GameSettings/Enemy", fileName = "EnemySpawnSettings")]
    public class EnemySpawnSettings : ScriptableObject
    {
        [SerializeField]
        private List<SpawnParameter> _spawnParameters;
        
        public List<SpawnParameter> SpawnParameters => _spawnParameters;
        
        [Serializable]
        public class SpawnParameter
        {
            public int enemyQuantity;
            public float spawnChance;
        }
    }
}
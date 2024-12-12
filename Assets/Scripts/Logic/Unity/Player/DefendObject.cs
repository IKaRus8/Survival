using System;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Unity.Player;
using UnityEngine;

namespace Logic.Unity.Player
{
    public class DefendObject : MonoBehaviour, IDefendObject
    {
        public float Health { get; private set; }
        public bool IsDead => Health <= 0;
        public Vector3 Position {get; private set;}

        private void Awake()
        {
            Position = transform.position;

            Health = 500f;
        }
        
        public void TakeDamage(float damage)
        {
            Health -= damage;
        }

        public void Heal(float healAmount)
        {
            Health += healAmount;
        }

        public UniTask Die()
        {
            throw new NotImplementedException();
        }
    }
}
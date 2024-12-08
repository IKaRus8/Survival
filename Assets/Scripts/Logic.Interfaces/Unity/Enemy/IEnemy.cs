using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using Data.Interfaces.Models.Attack;
using UnityEngine;

namespace Logic.Interfaces.Unity.Enemy
{
    public interface IEnemy : IDamageable
    {
        string Id { get; }
        Vector3 Position { get; }
        bool CanAttack { get; }
        IEnemyModel Model { get; }
        IAttackModel EnemyAttackModel { get; }
        bool Active { get; }

        void Initialize(IEnemyModel model, IAttackModel attackModel);
        void ReInitialize();
        void Move(Vector3 offset);
        void MoveTo(Vector3 newPosition);
        void Rotate(Vector3 direction);
        UniTask Attack();
    }
}
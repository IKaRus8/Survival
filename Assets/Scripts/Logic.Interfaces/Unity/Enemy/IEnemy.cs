using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using UnityEngine;

namespace Logic.Interfaces.Unity.Enemy
{
    public interface IEnemy : IDamageable
    {
        string Id { get; }
        Vector3 Position { get; }
        IEnemyModel Model { get; }

        void Initialize(IEnemyModel model);
        void Reset();
        void Move(Vector3 offset);
        void MoveTo(Vector3 newPosition);
        UniTask<float> Attack();
    }
}
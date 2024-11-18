using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using UnityEngine;

namespace Logic.Interfaces.Unity
{
    public interface IHero : IDamageable 
    {
        string Id { get; }
        float Speed { get; }
        Transform Transform { get; }
        Transform WeaponShootPoint { get; }
        IHeroModel Model { get; }
        
        void Initialize(IHeroModel model);
        void Move(Vector3 direction);
        void Rotate(Vector3 direction);
        UniTask<float> Attack();
    }
}

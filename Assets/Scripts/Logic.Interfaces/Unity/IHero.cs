using Cysharp.Threading.Tasks;
using Data.Interfaces.Models;
using Data.Interfaces.Models.Attack;
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
        IAttackModel HeroAttackModel { get; }
        
        void Initialize(IHeroModel model, IAttackModel attackModel);
        void Move(Vector3 direction);
        void Rotate(Vector3 direction);
        UniTask Attack();
    }
}

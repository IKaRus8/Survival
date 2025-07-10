using Cysharp.Threading.Tasks;
using Data.Interfaces.Enums;
using UnityEngine;

namespace Logic.Interfaces.Unity
{
    public interface IOrb : IEntity
    {
        OrbTypes OrbType { get; }
        bool IsTaken { get; }
        
        void MoveTo(Vector3 position);
        UniTask FlyTo(Vector3 position);
        void DestroyOrb();
    }
}
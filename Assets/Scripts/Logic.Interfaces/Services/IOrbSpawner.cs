using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic.Interfaces.Services
{
    public interface IOrbSpawner
    {
        UniTask SpawnOrb(string orbId, Vector3 position);
    }
}
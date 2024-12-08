using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic.Interfaces.Services.Level
{
    public interface IVfxService
    {
        UniTaskVoid ShowVfx(string vfxId, Vector3 position);
    }
}
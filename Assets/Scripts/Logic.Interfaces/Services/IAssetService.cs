using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Logic.Interfaces.Services
{
    public interface IAssetService
    {
        public UniTask<TAsset> LoadAssetAsync<TAsset>(string addressableKey);

        UniTask<GameObject> LoadAndInstantiateAsync(string addressableKey, Transform parent);

        UniTask<T> LoadAndInstantiateAsync<T>(string addressableKey, Transform parent);

        UniTask<T> LoadWithComponent<T>(string key) where T : Component;
    }
}
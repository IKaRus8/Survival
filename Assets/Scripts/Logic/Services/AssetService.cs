using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Logic.Services
{
    public class AssetService : IAssetService, IDisposable
    {
        private readonly List<AsyncOperationHandle> _cache = new ();

        public async UniTask <T> LoadAssetAsync<T>(string addressableKey)
        {
            var result = await Load<T>(addressableKey);
        
            return result;
        }

        public async UniTask<T> LoadWithComponent<T>(string key) where T : Component
        {
            var go = await Load<GameObject>(key);
            
            if (!go.TryGetComponent<T>(out var component))
            {
                throw new NullReferenceException($"Can't get {typeof(T).Name} from {go.name}");
            }

            return component;
        }

        public async UniTask<GameObject> LoadGameObjectAsync(string addressableKey)
        {
            return await Load<GameObject>(addressableKey);
        }

        private async UniTask<T> Load<T>(string addressableKey)
        {
            var asyncOperationHandle = Addressables.LoadAssetAsync<T>(addressableKey);
        
            _cache.Add(asyncOperationHandle);

            return await asyncOperationHandle.ToUniTask();
        }

        public async UniTask<GameObject> LoadAndInstantiateAsync(string addressableKey, Transform parent)
        {
            var asyncOperationHandle = Addressables.InstantiateAsync(addressableKey, parent);
            
            _cache.Add(asyncOperationHandle);

            return await asyncOperationHandle.ToUniTask();
        }

        public async UniTask<T> LoadAndInstantiateAsync<T>(string addressableKey, Transform parent)
        {
            var go = await LoadAndInstantiateAsync(addressableKey, parent);

            if (go == null)
            {
#if UNITY_EDITOR || DEBUG
                Debug.LogError($"Failed to load asset {addressableKey}");
#endif
                return default;
            }

            if (go.TryGetComponent<T>(out var component))
            {
                return component;
            }
            
#if UNITY_EDITOR || DEBUG
            Debug.LogError($"Failed to get component {nameof(T)}");
#endif
            return default;
        }

        private void ReleaseAsset()
        {
            foreach (var handle in _cache)
            {
                if (!handle.IsValid())
                {
                    continue;
                }
                
                Addressables.Release(handle);
            }
            
            _cache.Clear();
        }

        public void Dispose()
        {
            ReleaseAsset();
        }
    }
}
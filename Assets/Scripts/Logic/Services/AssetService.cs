using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Logic.Interfaces;
using UnityEngine.AddressableAssets;

namespace Logic.Services
{
    public class AssetService: IAssetService
    {
        private readonly Dictionary<string, object> _cache = new ();

        public async UniTask <TAsset> GetAssetAsync<TAsset>(string addressableKey, bool isCached = true) where TAsset : class
        {
            if (_cache.TryGetValue(addressableKey, out var result))
            {
                return result as TAsset;
            }

            result = await LoadNewAssetAsync<TAsset>(addressableKey, isCached);
        
            return (TAsset) result;
        }


        private async UniTask <TAsset> LoadNewAssetAsync<TAsset>(string addressableKey, bool isCached) where TAsset : class
        {
            var asyncOperationHandle = Addressables.LoadAssetAsync<TAsset>(addressableKey);
        
            _cache.Add(addressableKey, asyncOperationHandle);

            var result = await asyncOperationHandle;

            if (isCached == false)
            {
                ReleaseAsset(addressableKey);
            }

            return result;
        }

        private void ReleaseAsset(string addressableKey)
        {
            if (_cache.TryGetValue(addressableKey, out var asyncOperationHandle))
            {
                Addressables.Release(asyncOperationHandle);
            
                _cache.Remove(addressableKey);
            }
        }
    }
}

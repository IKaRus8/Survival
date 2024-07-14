using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetService: IAssetService
{
    private Dictionary<string, object> _cache = new ();

    public async Task<TAsset> GetAssetAsync<TAsset>(string addressableKey, bool isCached = true) where TAsset : class
    {
        if (_cache.TryGetValue(addressableKey, out var result))
        {
            return result as TAsset;
        }
        else
        {
            result = await LoadNewAssetAsync<TAsset>(addressableKey, isCached);
            return result as TAsset;
        }
    }


    private async Task<TAsset> LoadNewAssetAsync<TAsset>(string addressableKey, bool isCached) where TAsset : class
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

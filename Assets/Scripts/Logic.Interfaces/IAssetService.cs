using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IAssetService
{
    public UniTask <TAsset> GetAssetAsync<TAsset>(string addressableKey, bool isCached = true) where TAsset : class;
   
}
using System.Threading.Tasks;
using UnityEngine;

public interface IAssetService
{
    public abstract Task<TAsset> GetAssetAsync<TAsset>(string addressableKey, bool isCached = true) where TAsset : class;
   
}
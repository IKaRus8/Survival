using Cysharp.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IAssetService
    {
        public UniTask <TAsset> GetAssetAsync<TAsset>(string addressableKey) where TAsset : class;
   
    }
}
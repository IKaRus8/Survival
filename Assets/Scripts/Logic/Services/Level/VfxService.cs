using Cysharp.Threading.Tasks;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using UnityEngine;

namespace Logic.Services.Level
{
    public class VfxService : IVfxService
    {
        private readonly IAssetService _assetService;
        private readonly Transform _parent;

        public VfxService(
            IAssetService assetService,
            IBaseSceneObjectContainer objectContainer)
        {
            _assetService = assetService;
            
            _parent = objectContainer.VfxContainer;
        } 
        
        public async UniTaskVoid ShowVfx(string vfxId, Vector3 position)
        {
            var vfx = await _assetService.LoadAndInstantiateAsync(vfxId, _parent);
            
            vfx.transform.position = position;
            
            Object.Destroy(vfx, 1f);
        }
    }
}
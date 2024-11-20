using Cysharp.Threading.Tasks;
using UI.Interfaces.Popups;

namespace Logic.Interfaces.Services
{
    public interface IPopupSystem
    {
        UniTask<T> ShowPopup<T>(string key) where T : IPopupBase;
    }
}
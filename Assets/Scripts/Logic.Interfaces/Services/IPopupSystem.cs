using Cysharp.Threading.Tasks;

namespace Logic.Interfaces.Services
{
    public interface IPopupSystem
    {
        UniTask<T> ShowPopup<T>(string key);
    }
}
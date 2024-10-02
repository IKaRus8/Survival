using Cysharp.Threading.Tasks;

namespace Logic.Interfaces.Presenters
{
    public interface IGameEndedPopupPresenter
    {
        UniTask ShowPopup();
    }
}
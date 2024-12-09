using System;

namespace UI.Interfaces.Popups
{
    public interface IPopupBase
    {
        event Action OnClose;
        
        void Show();
        
        void Close();
    }
}
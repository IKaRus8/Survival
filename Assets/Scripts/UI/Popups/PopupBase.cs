using System;
using UI.Interfaces.Popups;
using UnityEngine;

namespace UI.Popups
{
    public abstract class PopupBase : MonoBehaviour, IPopupBase
    {
        public event Action OnClose;

        public void Show()
        {
            
        }

        public void Close()
        {
            OnClose?.Invoke();
            
            Destroy(gameObject);
        }
    }
}
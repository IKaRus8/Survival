using System;
using Logic.RuntimeData;
using R3;
using TMPro;
using UI.Interfaces.View;
using UnityEngine;

namespace UI.View
{
    public class LevelTimerView : MonoBehaviour, ILevelTimerView
    {
        [SerializeField]
        private TMP_Text _timerText;

        private TimerData _timerData;
        private IDisposable _disposable;

        public void Show(TimerData timerData)
        {
            gameObject.SetActive(true);
            
            _timerData = timerData;

            _disposable = timerData.SecondLeftRx.Subscribe(UpdateTimer);
        }

        private void UpdateTimer(int timeLeft)
        {
            _timerText.text = $"{timeLeft}";
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}
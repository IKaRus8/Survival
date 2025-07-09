using System;
using Logic.Interfaces.Services;
using Logic.Interfaces.Services.Level;
using Logic.RuntimeData;
using R3;
using UI.Interfaces.View;

namespace Logic.Services.Level
{
    public class LevelTimer : IDisposable
    {
        private const string LevelTimerId = "timer.level";
        private readonly TimeSpan _levelDuration = TimeSpan.FromSeconds(60);
        
        private readonly ITimerService _timerService;
        private readonly ILevelTimerView _levelTimerView;
        private readonly ILevelParametersHolder _levelParametersHolder;
        private readonly IGameOverService _gameOverService;

        private IDisposable _disposable;

        public TimerData Timer {get; private set;}

        public LevelTimer(
            ITimerService timerService,
            ILevelTimerView levelTimerView,
            ILevelParametersHolder levelParametersHolder,
            IGameOverService gameOverService)
        {
            _timerService = timerService;
            _levelTimerView = levelTimerView;
            _levelParametersHolder = levelParametersHolder;
            _gameOverService = gameOverService;

            if (_levelParametersHolder.Parameters.IsTimeLevel)
            {
                StartTimer();
            }
        }

        public void StartTimer()
        {
            Timer = _timerService.StartTimer(LevelTimerId, _levelDuration);
            
            _levelTimerView.Show(Timer);

            _disposable = Timer.IsOverRx.Subscribe(OnTimerFinished);
        }

        private void OnTimerFinished(bool isOver)
        {
            if (!isOver)
            {
                return;
            }
            
            _gameOverService.LevelComplete();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
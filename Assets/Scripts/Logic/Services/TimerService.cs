using System;
using System.Collections.Generic;
using System.Linq;
using Logic.Interfaces.Services;
using Logic.RuntimeData;
using R3;
using UnityEngine;

namespace Logic.Services
{
    public class TimerService : ITimerService, IDisposable
    {
        private readonly IDisposable _disposable;
        
        public List<TimerData> ActiveTimers { get; }
        
        public TimerService()
        {
            ActiveTimers = new List<TimerData>();

            _disposable = Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(Tick);
        }

        public TimerData StartTimer(string id, TimeSpan duration)
        {
            var timer = new TimerData(id, duration);
            
            ActiveTimers.Add(timer);
            
            return timer;
        }

        public TimerData GetTimer(string id)
        {
            var timer = ActiveTimers.FirstOrDefault(t => t.ID == id);

            if (timer == null)
            {
                Debug.LogWarning($"Timer with ID {id} does not exist");
            }
            
            return timer;
        }

        private void Tick(Unit _)
        {
            foreach (var timer in ActiveTimers)
            {
                timer.Tick();
            }
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}
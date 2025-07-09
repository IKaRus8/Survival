using System;
using System.Collections.Generic;
using Logic.RuntimeData;

namespace Logic.Interfaces.Services
{
    public interface ITimerService
    {
        List<TimerData> ActiveTimers { get; }

        TimerData StartTimer(string id, TimeSpan duration);
        TimerData GetTimer(string id);
    }
}
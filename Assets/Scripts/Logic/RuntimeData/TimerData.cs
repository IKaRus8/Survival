using System;
using R3;

namespace Logic.RuntimeData
{
    public class TimerData
    {
        public string ID { get; }
        public DateTime StartTime { get; }
        public TimeSpan Duration { get; private set; }
        
        public ReactiveProperty<bool> IsOverRx { get; }
        public ReactiveProperty<int> SecondLeftRx { get; }
        
        private float SecondsDuration => (float) Duration.TotalSeconds;

        public TimerData(string id, DateTime startTime, TimeSpan duration)
        {
            StartTime = startTime;
            ID = id;
            Duration = duration;
            
            SecondLeftRx = new ReactiveProperty<int>();
            SecondLeftRx.Value = (int) duration.TotalSeconds;
            
            IsOverRx = new ReactiveProperty<bool>();
        }

        public TimerData(string id, TimeSpan duration) : this(id, DateTime.UtcNow, duration)
        { }

        public void AddDuration(TimeSpan duration)
        {
            Duration += duration;
        }

        public void SubtractDuration(TimeSpan duration)
        {
            Duration -= duration;
        }

        public void Tick()
        {
            if (IsOverRx.Value)
            {
                return;
            }
            
            var timeSpent = DateTime.UtcNow - StartTime;

            SecondLeftRx.Value = (int) Duration.Subtract(timeSpent).TotalSeconds;

            if (SecondLeftRx.Value <= 0f)
            {
                IsOverRx.Value = true;

                SecondLeftRx.Value = 0;
            }
        }
    }
}
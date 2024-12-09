using System;
using System.Collections.Generic;
using Logic.Interfaces.Services.Level;
using Logic.Interfaces.Services.Level.Hero;

namespace Logic.Services.Level
{
    public class PauseService : IPauseService, IDisposable
    {
        private readonly IHeroDeathObserver _heroDeathObserver;
        private readonly IEnumerable<IPauseHandler> _pauseHandler;

        public PauseService(
            IHeroDeathObserver heroDeathObserver,
            IEnumerable<IPauseHandler> pauseHandler)
        {
            _heroDeathObserver = heroDeathObserver;
            _pauseHandler = pauseHandler;
            
            _heroDeathObserver.HeroDie += Pause;
        }
        
        public void Pause()
        {
            foreach (var handler in _pauseHandler)
            {
                handler.Pause();
            }
        }

        public void Resume()
        {
            foreach (var handler in _pauseHandler)
            {
                handler.Resume();
            }
        }

        public void Dispose()
        {
            _heroDeathObserver.HeroDie -= Pause;
        }
    }
}
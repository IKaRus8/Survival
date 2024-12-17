using System.Collections.Generic;
using Logic.Interfaces.Services.Level;

namespace Logic.Services.Level
{
    public class PauseService : IPauseService
    {
        private readonly IEnumerable<IPauseHandler> _pauseHandler;

        public PauseService(IEnumerable<IPauseHandler> pauseHandler)
        {
            _pauseHandler = pauseHandler;
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
    }
}
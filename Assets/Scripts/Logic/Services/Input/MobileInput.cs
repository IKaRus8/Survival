using Logic.Interfaces;
using R3;
using UnityEngine;

namespace Logic.Services.Input
{
    public class MobileInput : IInput
    {
        private readonly Joystick _joystick;
        private Vector2 _dir;
    
        public Vector2 Dir
        {
            get => _dir;
            set => _dir = value;
        }

        public MobileInput(Joystick joystick)
        {
            _joystick = joystick; 
        
            Observable.EveryUpdate().Subscribe(_ => TickUpdate());
        }

        public void TickUpdate()
        {
            _dir.x = _joystick.Horizontal;
            _dir.y = _joystick.Vertical;
        
            _dir.Normalize();
        }
    }
}
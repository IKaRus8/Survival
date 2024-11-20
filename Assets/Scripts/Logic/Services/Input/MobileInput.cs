using System;
using Logic.Interfaces.Unity;
using R3;
using UnityEngine;

namespace Logic.Services.Input
{
    public class MobileInput : IInput, IDisposable
    {
        private readonly Joystick _joystick;
        private readonly IDisposable _updateDisposable;
        
        public Vector3 Direction { get; private set; }

        public MobileInput(Joystick joystick)
        {
            _joystick = joystick; 
        
            _updateDisposable = Observable.EveryUpdate().Subscribe(TickUpdate);
        }

        private void TickUpdate(Unit _)
        {
            Direction = new Vector3(_joystick.Horizontal, 0f, _joystick.Vertical);
        
            Direction.Normalize();
        }

        public void Dispose()
        {
            _updateDisposable?.Dispose();
        }
    }
}
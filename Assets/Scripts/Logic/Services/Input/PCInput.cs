using Logic.Interfaces;
using UnityEngine;

namespace Logic.Services.Input
{
    public class PCInput : IInput
    {
        public Vector2 Dir
        {
            get { return dir; }
            set { dir = value; }
        }

        private Vector2 dir;   

        public void TickUpdate()
        {
            dir = (UnityEngine.Input.GetAxis("Horizontal") * Vector2.right + UnityEngine.Input.GetAxis("Vertical") * Vector2.up).normalized;
        }
    }
}

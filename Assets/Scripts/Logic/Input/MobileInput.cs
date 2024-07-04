using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : IInput
{
    private Joystick _joystick;
    public Vector2 Dir
    {
        get { return dir; }
        set { dir = value; }
    }

    public bool attack { get; set ; }

    private Vector2 dir;
    public Vector2 move; 

    public MobileInput(Joystick joystick)
    {
        _joystick = joystick;       
    }

    public void TickUpdate()
    {
        dir.x = _joystick.Horizontal;
        dir.y = _joystick.Vertical;
        dir.Normalize();
    }
}

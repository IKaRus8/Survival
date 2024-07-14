using UnityEngine;

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
        dir = (Input.GetAxis("Horizontal") * Vector2.right + Input.GetAxis("Vertical") * Vector2.up).normalized;
    }
}

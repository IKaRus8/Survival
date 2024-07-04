using UnityEditor.Build.Pipeline;
using UnityEngine;

public class PCInput : IInput
{
    public Vector2 Dir
    {
        get { return dir; }
        set { dir = value; }
    }

    private Vector2 dir;

    public bool attack { get; set; } 

    public void TickUpdate()
    {      
        dir = (Input.GetAxis("Horizontal") * Vector2.right + Input.GetAxis("Vertical") * Vector2.up).normalized;  
    }  
       
}

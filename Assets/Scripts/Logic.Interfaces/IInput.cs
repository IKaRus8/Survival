using UnityEngine;

public interface IInput 
{
     public Vector2 Dir { get; set; }        

     public void TickUpdate();
}

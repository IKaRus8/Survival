using UnityEngine;

public interface IInput 
{
     public Vector2 Dir { get; set; }   

     bool attack {get; set;}

     public void TickUpdate();
}

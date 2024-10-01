using UnityEngine;

namespace Logic.Interfaces
{
     public interface IInput 
     {
          public Vector2 Dir { get; set; }        

          public void TickUpdate();
     }
}

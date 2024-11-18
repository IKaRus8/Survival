using UnityEngine;

namespace Logic.Interfaces.Unity
{
     public interface IInput 
     {
          public Vector2 Dir { get; set; }        

          public void TickUpdate();
     }
}

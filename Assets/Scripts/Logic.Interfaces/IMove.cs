using UnityEngine;

namespace Logic.Interfaces
{
    public interface IMove 
    {
        void Move(Transform transform, Vector3 direction, float speed, float delta);    
    }
}

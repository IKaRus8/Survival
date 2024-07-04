using UnityEngine;

public class Entity : MonoBehaviour
{
    public EntityConfig config;

    public IMove mover;

    private void  Start()
    {
        mover = new Movable();
    }
}
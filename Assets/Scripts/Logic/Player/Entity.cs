using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Entity : MonoBehaviour
{
    public EntityConfig config;

    public IMove mover;

    /*[Inject]
    public void Construct(IMove mover)
    {
        this.mover=mover;        
    }*/

    private void  Start()
    {
        mover = new Movable();
    }
}
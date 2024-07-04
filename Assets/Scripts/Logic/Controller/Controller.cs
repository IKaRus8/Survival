using UnityEngine;
using Zenject;

public class Controller : MonoBehaviour, IController 
{
    private IInput _input;
    [SerializeField] private Entity _currentEntity;

    [Inject]

    public void Construct(IInput input, Player player)
    {
        _input = input;
        _currentEntity = player;
    }

    private void Update()
    {
        if (_currentEntity == null) return;

        _input.TickUpdate();  
        _currentEntity.mover.Move(_currentEntity.transform,  new Vector3(_input.Dir.x, 0 , _input.Dir.y),
        _currentEntity.config.baseSpeed, Time.deltaTime);      
    }

    public void SetEntity(Entity entity)
    {
        _currentEntity = entity;
    }

    public Entity GetCurrentEntity()
    {
        return _currentEntity;
    }
}


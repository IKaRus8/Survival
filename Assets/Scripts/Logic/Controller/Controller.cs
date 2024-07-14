using UnityEngine;
using Zenject;

public class Controller : MonoBehaviour, IController
{
    private IInput _input;
    [SerializeField] private IPlayer _player;
    private bool hasPlayer = false;

    [Inject]

    public void Construct(IInput input)
    {
        _input = input;        
    }

    public void SetPlayer(IPlayer player)
    {
        _player = player;       
        hasPlayer = true;
    }

    public IPlayer GetPlayer()
    {
        return _player;
    }

    private void Update()
    {     
        if(!hasPlayer) return;
        _input.TickUpdate();  
        _player.Move(new Vector3(_input.Dir.x, 0 , _input.Dir.y), _player.Speed, Time.deltaTime);      
    }
   
}


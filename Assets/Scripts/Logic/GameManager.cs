using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{    
    private ICreator<IPlayer> _playerCreator;
    private IController _controller;

    [Inject]
    public void Construct( ICreator<IPlayer> creator, IController controller)
    {        
        _playerCreator = creator;
        _controller = controller;
    }

    private async void Awake()
    {
        var player = await _playerCreator.CreateAsync();
        _controller.SetPlayer(player);
    }


}

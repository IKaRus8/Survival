using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public class GameManager 
{    
    private ICreator<IPlayer> _playerCreator;
    private IController _controller;    
    public  GameManager(ICreator<IPlayer> creator, IController controller)
    {        
        _playerCreator = creator;
        _controller = controller;
        InitPlayer();
    } 

    public async void InitPlayer()
    {
        var player = await _playerCreator.CreateAsync();
        _controller.SetPlayer(player);
    }
}

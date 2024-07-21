using UnityEngine;

public interface  IController 
{
    void SetPlayer(IPlayer player);
    IPlayer GetPlayer();
}

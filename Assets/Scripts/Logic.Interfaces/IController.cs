using UnityEngine;

public interface  IController 
{
    public void SetPlayer(IPlayer player);
    public IPlayer GetPlayer();
}

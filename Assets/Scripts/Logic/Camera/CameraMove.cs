using UnityEngine;
using Zenject;

public class CameraMove : MonoBehaviour
{ 
    private IController _controller;
    private Vector3 offset=new Vector3(0,10,-5);

    Transform _transform;

    [Inject]
    public void Construct(IController controller)
    {
        _controller= controller;
        _transform = transform;
    }   

    private  void LateUpdate()
    {       
        if(_controller.GetPlayer() == null) return;
       _transform.position = _controller.GetPlayer().Transform.position + offset;         
    }
}

using UnityEngine;
using Zenject;

public class CameraMove : MonoBehaviour
{ 
    private IController _controller;
    private Vector3 offset=new Vector3(0,10,-5);

    
    [Inject]
    public void Construct(IController controller)
    {
        _controller= controller;
    }
   

    private  void LateUpdate()
    {         
       if (_controller.GetCurrentEntity()== null) return;
       transform.position = _controller.GetCurrentEntity().transform.position + offset;         
    }
}

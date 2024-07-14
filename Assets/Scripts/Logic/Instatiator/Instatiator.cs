using UnityEngine;

public class Instatiator : MonoBehaviour, IInstatiator
{
    public object InstantiateAsset(object obj)
    {
        var intantiateObject = Instantiate(obj as GameObject);
        return intantiateObject;
    }   
}

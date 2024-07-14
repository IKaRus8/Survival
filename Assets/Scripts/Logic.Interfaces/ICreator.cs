using System.Threading.Tasks;
using UnityEngine;

public interface ICreator<T>
{
    Task<T> CreateAsync();
}

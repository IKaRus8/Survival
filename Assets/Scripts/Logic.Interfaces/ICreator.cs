using Cysharp.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface ICreator<T>
    {
        UniTask<T> CreateAsync();
    }
}

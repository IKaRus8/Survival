using Cysharp.Threading.Tasks;

public interface ICreator<T>
{
    UniTask<T> CreateAsync();
}

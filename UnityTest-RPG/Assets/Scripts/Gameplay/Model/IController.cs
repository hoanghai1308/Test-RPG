namespace Gameplay.Model
{
    using Cysharp.Threading.Tasks;

    public interface IController<TData>
    {
        UniTask Create(TData data);
    }
}
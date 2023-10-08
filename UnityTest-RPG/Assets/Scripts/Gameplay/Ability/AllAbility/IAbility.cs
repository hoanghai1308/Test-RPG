namespace Gameplay.Ability.AllAbility
{
    using Cysharp.Threading.Tasks;
    using Gameplay.Model;

    public interface IAbility
    {
        string  AbilityName { get; }
        UniTask Execute(IData attacker, IData hitAttack);
    }
}
namespace Gameplay.Ability.AllAbility
{
    using Cysharp.Threading.Tasks;
    using Gameplay.Model;

    public interface IAbility
    {
        string  AbilityName { get; }
        UniTask Execute(AbilityDataState abilityDataState,IData attacker, IData hitAttack);
    }
}
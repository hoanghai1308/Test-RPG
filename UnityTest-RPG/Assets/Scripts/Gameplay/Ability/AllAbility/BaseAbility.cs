namespace Gameplay.Ability.AllAbility
{
    using Cysharp.Threading.Tasks;
    using Gameplay.Model;

    public abstract class BaseAbility : IAbility
    {
        public abstract string AbilityName { get; }
        public abstract UniTask   Execute(IData attacker, IData hitAttack);
    }
}
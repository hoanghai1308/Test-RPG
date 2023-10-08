namespace Gameplay.Ability.AllAbility
{
    using Cysharp.Threading.Tasks;
    using Gameplay.Model;

    public class RangerAbility : BaseAbility
    {
        public override string  AbilityName                              => "RangerAttack";
        public override async UniTask Execute(IData attacker, IData hitAttack) { }
    }
}
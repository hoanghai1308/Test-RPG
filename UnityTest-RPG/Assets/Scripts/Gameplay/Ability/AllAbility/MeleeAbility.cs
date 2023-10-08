namespace Gameplay.Ability.AllAbility
{
    using Cysharp.Threading.Tasks;
    using Gameplay.Model;

    public class MeleeAbility : BaseAbility
    {
        public override string AbilityName                              => "MeleeAttack";

        public override async UniTask Execute(IData attacker, IData hitAttack)
        {
            
        }
    }
}
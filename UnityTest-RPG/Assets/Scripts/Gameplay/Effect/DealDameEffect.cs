namespace Gameplay.Effect
{
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using Gameplay.Model;
    using Zenject;

    public class DealDameEffect : BaseEffect
    {
        public override string EffectId => "DealDame";
        public DealDameEffect(SignalBus signalBus) : base(signalBus) { }

        protected override async UniTask Preprocess(AbilityRecord abilityRecord, IData attacker, IData hitAttack)
        {
            if (hitAttack.Health > 0)
                hitAttack.Health -= abilityRecord.Value;
        }
    }
}
namespace Gameplay.Effect
{
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using Gameplay.Model;
    using Gameplay.Signal;
    using Zenject;

    public abstract class BaseEffect : IEffect
    {
        private readonly SignalBus signalBus;
        public abstract  string    EffectId { get; }

        protected BaseEffect(SignalBus signalBus) { this.signalBus = signalBus; }

        public async void ApplyEffect(AbilityRecord abilityRecord, IData attacker, IData hitAttack)
        {
            await this.Preprocess(abilityRecord, attacker, hitAttack);
            this.NotifyDataChange(hitAttack);
        }

        protected abstract UniTask Preprocess(AbilityRecord abilityRecord, IData attacker, IData hitAttack);

        private void NotifyDataChange(IData data)
        {
            this.signalBus.Fire(new UpdateUnitDataSignal()
            {
                Data = data
            });
        }
    }
}
namespace Gameplay.Effect
{
    using System.Collections.Generic;
    using Blueprints;
    using Gameplay.Signal;
    using Gameplay.Systems;
    using Zenject;

    public class ProcessEffectSystem : BaseSystem
    {
        private readonly AbilityBlueprint            abilityBlueprint;
        private readonly SignalBus                   signalBus;
        private          Dictionary<string, IEffect> CachedEffect = new();

        public ProcessEffectSystem(SignalBus signalBus, List<IEffect> effects, AbilityBlueprint abilityBlueprint)
        {
            this.signalBus        = signalBus;
            this.abilityBlueprint = abilityBlueprint;

            foreach (var item in effects)
            {
                this.CachedEffect.Add(item.EffectId, item);
            }
        }

        public override void Initialize() { this.signalBus.Subscribe<ProcessEffectSignal>(this.ProcessEffect); }

        private void ProcessEffect(ProcessEffectSignal obj)
        {
            var abilityRecord = this.abilityBlueprint[obj.HitTargetModel.AbilityDataState.AbilityKey];

            if (this.CachedEffect.TryGetValue(abilityRecord.EffectId, out var record))
            {
                record.ApplyEffect(abilityRecord, obj.HitTargetModel.Attacker, obj.HitTargetModel.HitAttack);
            }
        }
    }
}
namespace Gameplay.Effect
{
    using Blueprints;
    using Gameplay.Model;

    public interface IEffect
    {
        string EffectId { get; }
        void   ApplyEffect(AbilityRecord abilityRecord,IData attacker,IData hitAttack);
    }
}
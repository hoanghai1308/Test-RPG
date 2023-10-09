namespace Gameplay.Model
{
    public class AbilityDataState
    {
        public string       AbilityKey { get; set; }
        public AbilityState State      { get; set; }
    }

    public enum AbilityState
    {
        Free,
        Ready,
        Triggered,
        Cooldown
    }
}
namespace Gameplay.Model
{
    public class HitTargetModel
    {
        public AbilityDataState AbilityDataState { get; set; } = new();
        public IData            Attacker         { get; set; }
        public IData            HitAttack        { get; set; }
    }
}
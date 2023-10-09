namespace Gameplay.Model
{
    using System.Collections.Generic;

    public interface IEnemy : IData
    {
        string                 Id                { get; set; }
        List<AbilityDataState> AbilityDataStates { get; set; }
        int                    AttackRange       { get; set; }
        EnemyState             State             { get; set; }
    }

    public enum EnemyState
    {
        None,
        Move,
        Attack,
        Attacking,
    }
}
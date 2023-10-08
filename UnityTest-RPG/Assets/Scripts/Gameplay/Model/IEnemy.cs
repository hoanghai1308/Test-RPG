namespace Gameplay.Model
{
    public interface IEnemy : IData
    {
        int        AttackRange { get; set; }
        EnemyState State       { get; set; }
    }

    public enum EnemyState
    {
        None,
        Move,
        Attack,
        Attacking,
    }
}
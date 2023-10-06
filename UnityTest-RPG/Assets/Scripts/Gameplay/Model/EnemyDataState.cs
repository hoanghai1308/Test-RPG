namespace Gameplay.Model
{
    using UnityEngine;

    public abstract class EnemyDataState : IEnemy
    {
        public string  PrefabKey       { get; set; }
        public int     Health          { get; set; }
        public int     Damage          { get; set; }
        public Vector3 CurrentPosition { get; set; }
        public int     AttackRange     { get; set; }
    }
}
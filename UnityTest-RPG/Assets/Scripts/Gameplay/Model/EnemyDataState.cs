namespace Gameplay.Model
{
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class EnemyDataState : IEnemy
    {
        public string                 Id                { get; set; }
        public string                 PrefabKey         { get; set; }
        public Transform              View              { get; set; }
        public int                    Health            { get; set; }
        public int                    Damage            { get; set; }
        public Vector3                CurrentPosition   { get; set; }
        public int                    AttackRange       { get; set; }
        public EnemyState             State             { get; set; }
        public List<AbilityDataState> AbilityDataStates { get; set; } = new();
    }
}
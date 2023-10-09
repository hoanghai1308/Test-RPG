namespace Gameplay.Model
{
    using UnityEngine;

    public class WeaponDataState : IData
    {
        public string           AbilityKey        { get; set; }
        public string           PrefabKey         { get; set; }
        public Transform        View              { get; set; }
        public int              Health            { get; set; }
        public int              Damage            { get; set; }
        public Vector3          CurrentPosition   { get; set; }
        public Transform        Parent            { get; set; }
        public IData            Owner             { get; set; }
        public AbilityDataState AbilityDataState { get; set; } = new();
    }
}
namespace Gameplay.Model
{
    using UnityEngine;

    public class PlayerDataState:IData
    {
        public string  PrefabKey       { get; set; }
        public int     Health          { get; set; }
        public int     Damage          { get; set; }
        public Vector3 CurrentPosition { get; set; }
    }
}
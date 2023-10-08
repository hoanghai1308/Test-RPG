namespace Gameplay.Model
{
    using Blueprints;
    using UnityEngine;

    public class PlayerDataState : IData
    {
        public PlayerBlueprintRecord PlayerBlueprintRecord { get; set; }
        public string                PrefabKey             { get; set; }
        public Transform             View                  { get; set; }
        public int                   Health                { get; set; }
        public int                   Damage                { get; set; }
        public Vector3               CurrentPosition       { get; set; }
    }
}
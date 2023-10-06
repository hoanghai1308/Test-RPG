namespace Gameplay.Model
{
    using UnityEngine;

    public interface IData
    {
        string  PrefabKey       { get; set; }
        int     Health          { get; set; }
        int     Damage          { get; set; }
        Vector3 CurrentPosition { get; set; }
    }
}
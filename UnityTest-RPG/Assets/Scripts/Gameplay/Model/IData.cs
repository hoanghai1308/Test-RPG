namespace Gameplay.Model
{
    using UnityEngine;

    public interface IUnit
    {
        
    }

    public interface IData:IUnit
    {
        string    PrefabKey       { get; set; }
        Transform View            { get; set; }
        int       Health          { get; set; }
        int       Damage          { get; set; }
        Vector3   CurrentPosition { get; set; }
    }
}
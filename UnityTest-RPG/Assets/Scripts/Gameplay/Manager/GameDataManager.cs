namespace Gameplay.Manager
{
    using System.Collections.Generic;
    using Gameplay.Controller;
    using Gameplay.Model;

    public class GameDataManager
    {
        public KeyValuePair<PlayerDataState, PlayerController> PlayerCached;
        public Dictionary<IData, object>                       CachedEnemy { get; set; } = new();
    }
}
namespace Gameplay.Manager
{
    using System.Collections.Generic;
    using Gameplay.Controller;
    using Gameplay.Model;

    public class GameDataManager
    {
        public KeyValuePair<PlayerDataState, PlayerController> PlayerCached;
        public Dictionary<IData, IController>                  CachedEnemy      { get; set; } = new();
        public List<IController>                               ToTalControllers { get; set; } = new();
        public List<object>                                    EnemyControllers { get; set; } = new();
    }
}
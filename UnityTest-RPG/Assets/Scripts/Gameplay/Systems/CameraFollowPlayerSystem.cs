namespace Gameplay.Systems
{
    using System.Linq;
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Manager;
    using Gameplay.Model;
    using UnityEngine;

    public class CameraFollowPlayerSystem : BaseSystem
    {
        private readonly ILogService     logger;
        private readonly GameDataManager gameDataManager;
        private          PlayerDataState playerDataState;
        private          Vector3         Offset;
        private          Transform       Maincam => Camera.main.transform;

        public CameraFollowPlayerSystem(ILogService logger, GameDataManager gameDataManager)
        {
            this.logger          = logger;
            this.gameDataManager = gameDataManager;
        }

        public override void LateTick()
        {
            base.LateTick();
            this.FindPlayer();

            if (this.playerDataState != null)
            {
                this.Maincam.transform.position = this.playerDataState.CurrentPosition + this.Offset;
            }
        }

        private void FindPlayer()
        {
            if (this.playerDataState != null) return;
            this.playerDataState = this.gameDataManager.PlayerCached.Key;

            if (this.playerDataState != null)
            {
                this.Offset = this.Maincam.transform.position - this.playerDataState.CurrentPosition;
            }
        }
    }
}
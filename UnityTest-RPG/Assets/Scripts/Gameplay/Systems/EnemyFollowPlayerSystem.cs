namespace Gameplay.Systems
{
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Manager;
    using Gameplay.Model;
    using UnityEngine;

    public class EnemyFollowPlayerSystem : BaseSystem
    {
        private readonly GameDataManager gameDataManager;
        private readonly ILogService     logger;

        public EnemyFollowPlayerSystem(GameDataManager gameDataManager, ILogService logger)
        {
            this.gameDataManager = gameDataManager;
            this.logger          = logger;
        }

        public override void Tick()
        {
            foreach (var item in this.gameDataManager.CachedEnemy)
            {
                var playerDataState = this.gameDataManager.PlayerCached.Key;
                var data            = (EnemyDataState)item.Key;
                var controller      = item.Value;

                //
                if (data.State == EnemyState.None)
                {
                    var distance  = Vector3.Distance(playerDataState.CurrentPosition, data.CurrentPosition);
                    var direction = playerDataState.CurrentPosition - data.CurrentPosition;

                    if (distance > data.AttackRange)
                    {
                        controller.OnMove(direction.normalized * 5f * Time.deltaTime);
                    }
                    else
                    {
                        data.State = EnemyState.Attack;
                    }
                }
            }
        }
    }
}
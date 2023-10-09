namespace Gameplay.Systems
{
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Controller;
    using Gameplay.Manager;
    using Gameplay.Model;
    using UnityEngine;
    using Zenject;

    public class AutoSpawnEnemySystem : BaseSystem
    {
        private readonly GameDataManager    gameDataManager;
        private readonly ILogService        logger;
        private readonly SignalBus          signalBus;
        private readonly EnemyBlueprint     enemyBlueprint;
        private readonly DiContainer        diContainer;
        private readonly MiscParamBlueprint miscParamBlueprint;
        private          bool               AllowSpawnEnemy = false;
        private          float              Time            = 0;

        public AutoSpawnEnemySystem(GameDataManager gameDataManager, ILogService logger, SignalBus signalBus, EnemyBlueprint enemyBlueprint, DiContainer diContainer,
            MiscParamBlueprint miscParamBlueprint)
        {
            this.gameDataManager    = gameDataManager;
            this.logger             = logger;
            this.signalBus          = signalBus;
            this.enemyBlueprint     = enemyBlueprint;
            this.diContainer        = diContainer;
            this.miscParamBlueprint = miscParamBlueprint;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.AllowSpawnEnemy = true;
        }

        public override void Tick()
        {
            base.Tick();
            this.CheckToSpawnEnemy();
        }

        private async void CheckToSpawnEnemy()
        {
            this.Time += UnityEngine.Time.deltaTime;

            if (!this.AllowSpawnEnemy || this.gameDataManager.PlayerCached.Key == null) return;

            if (this.Time < this.miscParamBlueprint.TimeAutoSpawnEnemy) return;
            this.Time = 0;
            var count = Random.Range(this.miscParamBlueprint.SpawnEnemyRate[0], this.miscParamBlueprint.SpawnEnemyRate[1]);
            // count = 1000;
            // if (this.gameDataManager.CachedEnemy.Count >= 1000) return;

            var playerDataState = this.gameDataManager.PlayerCached.Key;

            for (var i = 0; i < count; i++)
            {
                var isMelee = Random.Range(0, 2) == 0;
                var enemyRecord = this.enemyBlueprint[isMelee ? "MeleeEnemy" : "RangerEnemy"];

                if (isMelee)
                {
                    await this.CreateAndCacheEnemy<MeleeEnemyDataState, MeleeEnemyController>(enemyRecord, playerDataState);
                }
                else
                {
                    await this.CreateAndCacheEnemy<RangerEnemyDataState, RangerEnemyController>(enemyRecord, playerDataState);
                }
            }

            this.gameDataManager.TotalEnemyCount.Value = this.gameDataManager.CachedEnemy.Count;
        }

        private async UniTask CreateAndCacheEnemy<TData, TController>(EnemyRecord enemyRecord, PlayerDataState playerDataState)
            where TData : EnemyDataState, new() where TController : IController<TData>
        {
            var posX = Random.Range(this.miscParamBlueprint.SpawnRangerPosition[0], this.miscParamBlueprint.SpawnRangerPosition[1]) * Mathf.Sign(Random.Range(-1f, 1f));
            var posZ = Random.Range(this.miscParamBlueprint.SpawnRangerPosition[0], this.miscParamBlueprint.SpawnRangerPosition[1]) * Mathf.Sign(Random.Range(-1f, 1f));

            var data = new TData
            {
                Id          = enemyRecord.Id,
                PrefabKey   = enemyRecord.PrefabKey,
                Health      = enemyRecord.Health,
                Damage      = enemyRecord.Damage,
                AttackRange = enemyRecord.AttackRange,
                CurrentPosition = playerDataState.CurrentPosition +
                                  new Vector3(posX, 0, posZ)
            };

            var controller = (TController)this.gameDataManager.EnemyControllers.Find(x => x is TController { IsFree: true });

            if (controller == null)
            {
                controller = this.diContainer.Instantiate<TController>();
            }

            await controller.Create(data);
            this.gameDataManager.CachedEnemy.Add(data, controller);

            if (!this.gameDataManager.EnemyControllers.Contains(controller))
            {
                this.gameDataManager.EnemyControllers.Add(controller);
                this.gameDataManager.ToTalControllers.Add(controller);
            }
        }
    }
}
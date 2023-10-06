namespace Gameplay.Systems
{
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using Gameplay.Controller;
    using Gameplay.Manager;
    using Gameplay.Model;
    using UnityEngine;
    using Zenject;

    public class AutoSpawnEnemySystem : BaseSystem
    {
        private readonly GameDataManager    gameDataManager;
        private readonly EnemyBlueprint     enemyBlueprint;
        private readonly DiContainer        diContainer;
        private readonly MiscParamBlueprint miscParamBlueprint;
        private          bool               AllowSpawnEnemy = false;
        private          float              Time            = 0;

        public AutoSpawnEnemySystem(GameDataManager gameDataManager, EnemyBlueprint enemyBlueprint, DiContainer diContainer, MiscParamBlueprint miscParamBlueprint)
        {
            this.gameDataManager    = gameDataManager;
            this.enemyBlueprint     = enemyBlueprint;
            this.diContainer        = diContainer;
            this.miscParamBlueprint = miscParamBlueprint;
        }

        public override async void Initialize()
        {
            base.Initialize();
            await UniTask.Delay(1000);
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
            var count           = Random.Range(5, 10);
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
        }

        private async UniTask CreateAndCacheEnemy<TData, TController>(EnemyRecord enemyRecord, PlayerDataState playerDataState)
            where TData : IEnemy, new() where TController : IController<TData>
        {
            var posX = Random.Range(30f, 50f) * Mathf.Sign(Random.Range(-1f, 1f));
            var posZ = Random.Range(30f, 50f) * Mathf.Sign(Random.Range(-1f, 1f));

            var data = new TData
            {
                PrefabKey   = enemyRecord.PrefabKey,
                Health      = enemyRecord.Health,
                Damage      = enemyRecord.Damage,
                AttackRange = enemyRecord.AttackRange,
                CurrentPosition = playerDataState.CurrentPosition +
                                  new Vector3(posX, 0, posZ)
            };

            var controller = this.diContainer.Instantiate<TController>();
            this.gameDataManager.CachedEnemy.Add(data, controller);
            await controller.Create(data);
        }
    }
}
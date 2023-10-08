namespace Gameplay.Systems
{
    using Gameplay.Manager;
    using Gameplay.Model;

    public class EnemyAttackPlayerSystem : BaseSystem
    {
        private readonly GameDataManager gameDataManager;

        public EnemyAttackPlayerSystem(GameDataManager gameDataManager) { this.gameDataManager = gameDataManager; }

        public override void Tick()
        {
            base.Tick();

            foreach (var item in this.gameDataManager.CachedEnemy)
            {
                var data       = (EnemyDataState)item.Key;
                var controller = (IEnemyController)item.Value;

                if (data.State == EnemyState.Attack)
                {
                    controller.OnAttack();
                }
            }
        }
    }
}
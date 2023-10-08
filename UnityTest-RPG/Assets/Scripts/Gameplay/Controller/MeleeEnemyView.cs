namespace Gameplay.Controller
{
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Model;

    public class MeleeEnemyView : EnemyView
    {
    }

    public class MeleeEnemyController : EnemyController<MeleeEnemyDataState, MeleeEnemyView>
    {
        private readonly ILogService logger;
        public MeleeEnemyController(IGameAssets gameAsset, ILogService logger) : base(gameAsset) { this.logger = logger; }

        public override void OnAttack()
        {
            base.OnAttack();
        }
    }
}
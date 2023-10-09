namespace Gameplay.Controller
{
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Manager;
    using Gameplay.Model;

    public class MeleeEnemyView : EnemyView
    {
    }

    public class MeleeEnemyController : EnemyController<MeleeEnemyDataState, MeleeEnemyView>
    {
        private readonly ILogService logger;

        public MeleeEnemyController(IGameAssets gameAsset, GameDataManager gameDataManager) : base(gameAsset, gameDataManager)
        {
        }
    }
}
namespace Gameplay.Controller
{
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Manager;
    using Gameplay.Model;

    public class RangerEnemyView : EnemyView
    {
    }

    public class RangerEnemyController : EnemyController<RangerEnemyDataState, RangerEnemyView>
    {
        public RangerEnemyController(IGameAssets gameAsset, GameDataManager gameDataManager) : base(gameAsset, gameDataManager) { }
    }
}
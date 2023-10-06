namespace Gameplay.Controller
{
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Model;

    public class MeleeEnemyView : EnemyView
    {
    }

    public class MeleeEnemyController : EnemyController<MeleeEnemyDataState, MeleeEnemyView>
    {
        public MeleeEnemyController(IGameAssets gameAsset) : base(gameAsset) { }
    }
}
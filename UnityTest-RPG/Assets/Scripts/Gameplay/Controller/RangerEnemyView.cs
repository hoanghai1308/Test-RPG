namespace Gameplay.Controller
{
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Model;

    public class RangerEnemyView : EnemyView
    {
    }

    public class RangerEnemyController : EnemyController<RangerEnemyDataState, RangerEnemyView>
    {
        public RangerEnemyController(IGameAssets gameAsset) : base(gameAsset) { }

        public override void OnAttack()
        {
            base.OnAttack();
        }
    }
}
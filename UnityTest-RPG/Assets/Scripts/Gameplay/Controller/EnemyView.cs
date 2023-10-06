namespace Gameplay.Controller
{
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Model;

    public class EnemyView:BaseUnitView
    {
        
    }
    
    public class EnemyController: BaseUnitController<EnemyDataState,EnemyView>
    {
        public EnemyController(IGameAssets gameAsset) : base(gameAsset)
        {
        }
    }
}
namespace Gameplay.Controller
{
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Model;

    public abstract class EnemyView : BaseUnitView
    {
    }

    public abstract class EnemyController<TData, TView> : BaseUnitController<TData, TView> where TData : IEnemy where TView : EnemyView
    {
        protected EnemyController(IGameAssets gameAsset) : base(gameAsset) { }

        public override async UniTask Create(TData data)
        {
            await base.Create(data);
            this.view.transform.position = data.CurrentPosition;
        }
    }
}
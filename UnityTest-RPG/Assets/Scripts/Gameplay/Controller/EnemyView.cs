namespace Gameplay.Controller
{
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Model;
    using UnityEngine;

    public abstract class EnemyView : BaseUnitView
    {
    }

    public abstract class EnemyController<TData, TView> : BaseUnitController<TData, TView>, IEnemyController where TData : IEnemy where TView : EnemyView
    {
        protected EnemyController(IGameAssets gameAsset) : base(gameAsset) { }

        public override async UniTask Create(TData data)
        {
            await base.Create(data);
            this.view.transform.position = data.CurrentPosition;
        }

        public override void OnMove(Vector3 direction)
        {
            base.OnMove(direction);
            this.LookAt(direction);

            this.PlayAnimation("Run");
        }

        public virtual void OnAttack()
        {
            this.PlayAnimation("Attack");
            this.Data.State = EnemyState.None;
        }
    }
}
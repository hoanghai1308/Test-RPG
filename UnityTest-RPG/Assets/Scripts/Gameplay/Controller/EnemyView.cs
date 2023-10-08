namespace Gameplay.Controller
{
    using System;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Model;
    using UnityEngine;

    public abstract class EnemyView : BaseUnitView
    {
    }

    public abstract class EnemyController<TData, TView> : BaseUnitController<TData, TView>, IEnemyController where TData : IEnemy where TView : EnemyView
    {
        private bool isFinishAttack;
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

        public virtual async void OnAttack()
        {
            this.Data.State     = EnemyState.Attacking;
            this.isFinishAttack = false;
            this.PlayAnimation("Attack01");
            await UniTask.WaitUntil(() => this.isFinishAttack);
            this.PlayAnimation("IdleBattle");
            await UniTask.Delay(1000);
            this.Data.State = EnemyState.None;
        }

        protected override void OnAnimationFinished(string obj)
        {
            if (obj.Equals("Attack01"))
            {
                this.isFinishAttack = true;
            }
        }
    }
}
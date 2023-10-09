namespace Gameplay.Controller
{
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Ability;
    using Gameplay.Manager;
    using Gameplay.Model;
    using UnityEngine;
    using Zenject;

    public abstract class EnemyView : BaseUnitView
    {
    }

    public abstract class EnemyController<TData, TView> : BaseUnitController<TData, TView>, IEnemyController where TData : IEnemy where TView : EnemyView
    {
        private readonly   GameDataManager gameDataManager;
        private            bool            isFinishAttack;
        [Inject] protected EnemyBlueprint  EnemyBlueprint;
        protected EnemyController(IGameAssets gameAsset, GameDataManager gameDataManager) : base(gameAsset) { this.gameDataManager = gameDataManager; }

        public override async UniTask Create(TData data)
        {
            await base.Create(data);

            foreach (var abilityId in this.EnemyBlueprint[this.Data.Id].AbilityList)
            {
                this.Data.AbilityDataStates.Add(this.CreateAbilityDataState(abilityId));
            }

            this.view.transform.position = data.CurrentPosition;
        }

        protected override void DetectDead()
        {
            base.DetectDead();

            if (!this.Data.IsDead) return;
            this.gameDataManager.TotalEnemyCount.Value--;
            this.gameDataManager.CachedEnemy.Remove(this.Data);
            this.Logger.LogWithColor($"On Enemy Dead {this.Data.Id}",Color.cyan);
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
            await this.TriggerAbility();
            this.PlayAnimation("IdleBattle");
            await UniTask.Delay(1000);
            this.Data.State = EnemyState.None;
        }

        private async UniTask TriggerAbility() { await this.ProcessAbility(true, this.Data.AbilityDataStates, this.Data, this.gameDataManager.PlayerCached.Key); }

        protected override void OnAnimationFinished(string obj)
        {
            if (obj.Equals("Attack01"))
            {
                this.isFinishAttack = true;
            }
        }
    }
}
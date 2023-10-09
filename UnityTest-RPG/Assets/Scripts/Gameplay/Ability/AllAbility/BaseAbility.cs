namespace Gameplay.Ability.AllAbility
{
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.Utilities.LogService;
    using GameFoundation.Scripts.Utilities.ObjectPool;
    using Gameplay.Controller;
    using Gameplay.Model;
    using UnityEngine;
    using Zenject;

    public abstract class BaseAbility : IAbility
    {
        [Inject] protected ILogService      Logger;
        [Inject] protected AbilityBlueprint AbilityBlueprint;
        [Inject] protected IGameAssets      GameAssets;
        [Inject] private   DiContainer      diContainer;
        public abstract    string           AbilityName { get; }
        public abstract    UniTask          Execute(AbilityDataState abilityDataState, IData attacker, IData hitAttack);

        protected async void CreateHitTarget(AbilityDataState abilityDataState, IData attacker, IData hitAttack)
        {
            var hitTarget = await this.GameAssets.LoadAssetAsync<GameObject>(this.AbilityBlueprint[abilityDataState.AbilityKey].HitTarget);
            var view      = hitTarget.Spawn();
            this.diContainer.InjectGameObject(view);
            var script = view.GetComponent<HitTargetView>();
            script.HitTargetModel.Attacker         = attacker;
            script.HitTargetModel.AbilityDataState = abilityDataState;
            script.HitTargetModel.HitAttack        = hitAttack;
            view.transform.position                = hitAttack.View.transform.position;
            await UniTask.Delay(200);
            view.Recycle();
        }
    }
}
namespace Gameplay.Controller
{
    using System.Collections.Generic;
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Ability;
    using Gameplay.Model;
    using Gameplay.Signal;
    using UnityEngine;

    public class SplashWeaponView : BaseUnitView
    {
    }

    public class SplashWeaponController : BaseUnitController<WeaponDataState, SplashWeaponView>
    {
        private readonly ILogService          logger;
        private readonly ProcessAbilitySystem processAbilitySystem;
        private readonly AbilityBlueprint     abilityBlueprint;
        private          bool                 ActiveAbility { get; set; }

        public SplashWeaponController(IGameAssets gameAsset, ILogService logger, ProcessAbilitySystem processAbilitySystem, AbilityBlueprint abilityBlueprint) : base(gameAsset)
        {
            this.logger               = logger;
            this.processAbilitySystem = processAbilitySystem;
            this.abilityBlueprint     = abilityBlueprint;
        }

        public override async UniTask Create(WeaponDataState data)
        {
            await base.Create(data);
            this.view.transform.SetParent(data.Parent);
            this.view.gameObject.SetActive(false);
            this.ActiveAbility = true;
            var abilityState = this.CreateAbilityDataState(this.Data.AbilityKey);
            this.Data.AbilityDataState = abilityState;
            this.ProcessAbility(this.ActiveAbility, new List<AbilityDataState>() { this.Data.AbilityDataState }, this.Data, null).Forget();
        }

        protected override void OnTriggerEnter(Collider obj)
        {
            base.OnTriggerEnter(obj);
            var script = obj.GetComponent<BaseUnitView>();

            if (script == null || script.Data.IsDead) return;

            this.SignalBus.Fire(new ProcessEffectSignal()
            {
                HitTargetModel = new HitTargetModel()
                {
                    Attacker         = this.Data,
                    HitAttack        = script.Data,
                    AbilityDataState = this.Data.AbilityDataState
                }
            });

            this.logger.Log($"{this.view.gameObject.name} Hit {obj.name}");
        }

        public override void Dispose()
        {
            base.Dispose();
            this.ActiveAbility = false;
        }
    }
}
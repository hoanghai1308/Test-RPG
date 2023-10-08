namespace Gameplay.Controller
{
    using System;
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.Utilities.LogService;
    using Gameplay.Ability;
    using Gameplay.Model;
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
            this.ProcessAbility();
        }

        private async void ProcessAbility()
        {
            if (!this.ActiveAbility) return;
            await UniTask.Delay(TimeSpan.FromSeconds(this.abilityBlueprint[this.Data.AbilityKey].TimeFrequency));
            await this.processAbilitySystem.ProcessAbility(this.Data.AbilityKey, this.Data, null);
            this.ProcessAbility();
        }

        protected override void OnTriggerEnter(Collider obj)
        {
            base.OnTriggerEnter(obj);
            this.logger.Log(obj.name);
        }

        public override void Dispose()
        {
            base.Dispose();
            this.ActiveAbility = false;
        }
    }
}
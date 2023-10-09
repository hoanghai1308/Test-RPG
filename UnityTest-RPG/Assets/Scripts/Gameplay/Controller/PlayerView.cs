namespace Gameplay.Controller
{
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Manager;
    using Gameplay.Model;
    using Gameplay.Signal;
    using UnityEngine;
    using Zenject;

    public class PlayerView : BaseUnitView
    {
    }

    public class PlayerController : BaseUnitController<PlayerDataState, PlayerView>
    {
        private readonly SignalBus       signalBus;
        private readonly GameDataManager gameDataManager;
        private readonly WeaponBlueprint weaponBlueprint;
        private readonly DiContainer     diContainer;

        public PlayerController(IGameAssets gameAsset, SignalBus signalBus, GameDataManager gameDataManager, WeaponBlueprint weaponBlueprint, DiContainer diContainer) : base(gameAsset)
        {
            this.signalBus       = signalBus;
            this.gameDataManager = gameDataManager;
            this.weaponBlueprint = weaponBlueprint;
            this.diContainer     = diContainer;
        }

        public override async UniTask Create(PlayerDataState data)
        {
            await base.Create(data);

            this.view.transform.position = Vector3.zero;

            foreach (var weapon in this.Data.PlayerBlueprintRecord.WeaponList)
            {
                await this.CreateWeapon(weapon.Key, weapon.Value);
            }
        }

        //public here when we want to create from an other 
        public async UniTask CreateWeapon(string weapontype, string weaponKey)
        {
            var dataState = new WeaponDataState
            {
                PrefabKey  = this.weaponBlueprint[weaponKey].PrefabKey,
                Owner      = this.Data,
                Parent     = this.view.transform,
                AbilityKey = this.weaponBlueprint[weaponKey].Ability
            };

            //Todo Spilit to Know Exact WeaponController
            switch (weapontype)
            {
                case "Splash":
                    var weaponController = this.diContainer.Instantiate<SplashWeaponController>();
                    await weaponController.Create(dataState);
                    this.gameDataManager.ToTalControllers.Add(weaponController);

                    break;
            }
        }

        protected override void OnTriggerEnter(Collider obj)
        {
            var script = obj.GetComponent<HitTargetView>();

            if (script != null)
            {
                this.signalBus.Fire(new ProcessEffectSignal()
                {
                    HitTargetModel = script.HitTargetModel
                });
            }
        }

        protected override void OnUpdateData()
        {
            if (this.Data.Health <= 0)
            {
                this.Logger.Log($"On player Dead");
            }
        }

        public override void OnMove(Vector3 direction)
        {
            base.OnMove(direction);
            this.LookAt(direction);
            this.PlayAnimation("Run");
        }

        public void OnStand() { this.PlayAnimation("IdleBattle"); }
    }
}
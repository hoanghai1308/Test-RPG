namespace Gameplay.Controller
{
    using Blueprints;
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Manager;
    using Gameplay.Model;
    using UnityEngine;
    using Zenject;

    public class PlayerView : BaseUnitView
    {
    }

    public class PlayerController : BaseUnitController<PlayerDataState, PlayerView>
    {
        private readonly GameDataManager  gameDataManager;
        private readonly WeaponBlueprint  weaponBlueprint;
        private readonly DiContainer      diContainer;

        public PlayerController(IGameAssets gameAsset,GameDataManager gameDataManager, WeaponBlueprint weaponBlueprint, DiContainer diContainer) : base(gameAsset)
        {
            this.gameDataManager  = gameDataManager;
            this.weaponBlueprint  = weaponBlueprint;
            this.diContainer      = diContainer;
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

        public async UniTask CreateWeapon(string weapontype, string weaponKey)
        {
            var dataState = new WeaponDataState
            {
                PrefabKey = this.weaponBlueprint[weaponKey].PrefabKey,
                Owner     = this.Data,
                Parent    = this.view.transform,
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

        public override void OnMove(Vector3 direction)
        {
            base.OnMove(direction);
            this.LookAt(direction);
        }
    }
}
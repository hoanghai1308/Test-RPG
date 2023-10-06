namespace Gameplay.Controller
{
    using Cysharp.Threading.Tasks;
    using GameFoundation.Scripts.AssetLibrary;
    using Gameplay.Model;
    using UnityEngine;

    public class PlayerView : BaseUnitView
    {
    }

    public class PlayerController : BaseUnitController<PlayerDataState, PlayerView>
    {
        float turnSmoothVelocity;
        float turnSmoothTime = 0.15f;
        public PlayerController(IGameAssets gameAsset) : base(gameAsset) { }

        public override async UniTask Create(PlayerDataState data)
        {
            await base.Create(data);
            this.view.transform.position = Vector3.zero;
        }

        public override void OnMove(Vector3 direction)
        {
            base.OnMove(direction);
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle       = Mathf.SmoothDampAngle(this.view.transform.eulerAngles.y, targetAngle, ref this.turnSmoothVelocity, this.turnSmoothTime);
            this.view.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
}
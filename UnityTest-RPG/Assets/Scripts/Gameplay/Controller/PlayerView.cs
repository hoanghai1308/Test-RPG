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
        public PlayerController(IGameAssets gameAsset) : base(gameAsset) { }

        public override async UniTask Create(PlayerDataState data)
        {
            await base.Create(data);
            this.view.transform.position = Vector3.zero;
        }

        public override void OnMove(Vector3 position)
        {
            base.OnMove(position);

            if (position != Vector3.zero)
            {
                var newRotation = Quaternion.LookRotation(Vector3.up, position.normalized);
                this.view.transform.rotation = Quaternion.Slerp(this.view.transform.rotation, newRotation, 2 * Time.deltaTime);
            }
        }
    }
}
namespace Gameplay.Systems
{
    using Gameplay.Manager;
    using UnityEngine;

    public class ControlPlayerMovementSystem : BaseSystem
    {
        private readonly GameDataManager gameDataManager;
        private          float           moveSpeed = 10.0f;

        public ControlPlayerMovementSystem(GameDataManager gameDataManager) { this.gameDataManager = gameDataManager; }

        public override void Tick()
        {
            if (this.gameDataManager.PlayerCached.Key == null) return;
            var horizontalInput = Input.GetAxis("Horizontal"); // Left/Right
            var verticalInput   = Input.GetAxis("Vertical"); // Up/Down

            var direction = new Vector3(horizontalInput, 0, verticalInput) * this.moveSpeed * Time.deltaTime;

            if (direction.normalized.magnitude >= 0.1f)
                this.gameDataManager.PlayerCached.Value.OnMove(direction);
            else
            {
                this.gameDataManager.PlayerCached.Value.OnStand();
            }
        }
    }
}
namespace Gameplay.Systems
{
    using Gameplay.Manager;
    using UnityEngine;
    using Zenject;

    public class ControlPlayerMovementSystem : ISystem, ITickable
    {
        private readonly GameDataManager gameDataManager;
        private          float           moveSpeed = 20.0f;

        public ControlPlayerMovementSystem(GameDataManager gameDataManager) { this.gameDataManager = gameDataManager; }

        public void Tick()
        {
            if (this.gameDataManager.PlayerCached.Key == null) return;
            var horizontalInput = Input.GetAxis("Horizontal"); // Left/Right
            var verticalInput   = Input.GetAxis("Vertical"); // Up/Down

            // Calculate the new position
            var movement = new Vector3(horizontalInput, 0, verticalInput) * this.moveSpeed * Time.deltaTime;

            this.gameDataManager.PlayerCached.Value.OnMove(movement);
        }
    }
}
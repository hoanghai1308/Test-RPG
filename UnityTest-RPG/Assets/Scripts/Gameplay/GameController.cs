namespace Gameplay
{
    using System.Collections.Generic;
    using System.Linq;
    using Blueprints;
    using Gameplay.Controller;
    using Gameplay.Manager;
    using Gameplay.Model;
    using Zenject;

    public class GameController : IInitializable
    {
        private readonly GameDataManager    gameDataManager;
        private readonly DiContainer        diContainer;
        private readonly PlayerBlueprint    playerBlueprint;
        private readonly MiscParamBlueprint miscParamBlueprint;

        public GameController(GameDataManager gameDataManager, DiContainer diContainer, PlayerBlueprint playerBlueprint, MiscParamBlueprint miscParamBlueprint)
        {
            this.gameDataManager    = gameDataManager;
            this.diContainer        = diContainer;
            this.playerBlueprint    = playerBlueprint;
            this.miscParamBlueprint = miscParamBlueprint;
        }

        public void Initialize() { this.CreatePlayer(); }

        private async void CreatePlayer()
        {
            var currentPlayerLevel = 0;

            var data = new PlayerDataState()
            {
                PrefabKey             = this.miscParamBlueprint.PlayerPrefab,
                Damage                = this.playerBlueprint.ElementAt(currentPlayerLevel).Value.Damage,
                Health                = this.playerBlueprint.ElementAt(currentPlayerLevel).Value.Health,
                PlayerBlueprintRecord = this.playerBlueprint.ElementAt(currentPlayerLevel).Value
            };

            var controller = this.diContainer.Instantiate<PlayerController>();
            await controller.Create(data);
            this.gameDataManager.PlayerCached = new KeyValuePair<PlayerDataState, PlayerController>(data, controller);
            this.gameDataManager.ToTalControllers.Add(controller);
        }
    }
}
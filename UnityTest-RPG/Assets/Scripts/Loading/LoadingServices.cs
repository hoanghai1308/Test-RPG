namespace Loading
{
    using BlueprintFlow.BlueprintControlFlow;
    using GameFoundation.Scripts.AssetLibrary;
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
    using UnityEngine;
    using Zenject;

    public class LoadingServices : IInitializable
    {
        private readonly SceneDirector          sceneDirector;
        private readonly IGameAssets            gameAssets;
        private readonly BlueprintReaderManager blueprintReaderManager;

        public LoadingServices(SceneDirector sceneDirector, IGameAssets gameAssets, BlueprintReaderManager blueprintReaderManager)
        {
            this.sceneDirector          = sceneDirector;
            this.gameAssets             = gameAssets;
            this.blueprintReaderManager = blueprintReaderManager;
        }

        public async void Initialize()
        {
             this.gameAssets.PreloadAsync<GameObject>("2.Play", "melee", "ranger", "player", "SplashWeapon");
            await this.blueprintReaderManager.LoadBlueprint();
            this.sceneDirector.LoadSingleSceneAsync("2.Play");
        }
    }
}
namespace Loading
{
    using BlueprintFlow.BlueprintControlFlow;
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
    using Zenject;

    public class LoadingServices : IInitializable
    {
        private readonly SceneDirector          sceneDirector;
        private readonly BlueprintReaderManager blueprintReaderManager;

        public LoadingServices(SceneDirector sceneDirector, BlueprintReaderManager blueprintReaderManager)
        {
            this.sceneDirector          = sceneDirector;
            this.blueprintReaderManager = blueprintReaderManager;
        }

        public async void Initialize()
        {
            await this.blueprintReaderManager.LoadBlueprint();
            this.sceneDirector.LoadSingleSceneAsync("2.Play");
        }
    }
}
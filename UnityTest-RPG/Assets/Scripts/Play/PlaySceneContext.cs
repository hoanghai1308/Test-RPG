namespace Play
{
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;
    using GameFoundation.Scripts.UIModule.Utilities;
    using Gameplay;

    public class PlaySceneContext : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            GamePlayInstaller.Install(this.Container);
            this.Container.InitScreenManually<PlayScreenPresenter>();
        }
    }
}
namespace Loading
{
    using GameFoundation.Scripts.UIModule.ScreenFlow.Managers;

    public class LoadingSceneContext : BaseSceneInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            this.Container.BindInterfacesAndSelfTo<LoadingServices>().AsCached().NonLazy();
        }
    }
}
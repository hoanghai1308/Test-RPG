namespace Gameplay
{
    using GameFoundation.Scripts.Utilities.Extension;
    using Gameplay.Manager;
    using Gameplay.Systems;
    using Zenject;

    public class GamePlayInstaller : Installer<GamePlayInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.Bind<GameDataManager>().AsCached();
            this.Container.BindInterfacesAndSelfToAllTypeDriveFrom<ISystem>();
            this.Container.BindInterfacesAndSelfTo<GameController>().AsCached().NonLazy();
        }
    }
}
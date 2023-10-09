namespace Gameplay
{
    using GameFoundation.Scripts.Utilities.Extension;
    using Gameplay.Ability;
    using Gameplay.Ability.AllAbility;
    using Gameplay.Effect;
    using Gameplay.Manager;
    using Gameplay.Signal;
    using Gameplay.Systems;
    using Zenject;

    public class GamePlayInstaller : Installer<GamePlayInstaller>
    {
        public override void InstallBindings()
        {
            this.Container.DeclareSignal<ProcessEffectSignal>();
            this.Container.DeclareSignal<UpdateUnitDataSignal>();
            this.Container.Bind<GameDataManager>().AsCached();
            this.Container.BindInterfacesAndSelfToAllTypeDriveFrom<ISystem>();
            this.Container.BindInterfacesAndSelfToAllTypeDriveFrom<IAbility>();
            this.Container.BindInterfacesAndSelfToAllTypeDriveFrom<IEffect>();
            this.Container.Bind<ProcessAbilitySystem>().AsCached().NonLazy();
            this.Container.BindInterfacesAndSelfTo<GameController>().AsCached().NonLazy();
        }
    }
}
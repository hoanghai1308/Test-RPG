using System;
using GameFoundation.Scripts;
using GameFoundation.Scripts.Interfaces;
using GameFoundation.Scripts.Utilities.Extension;
using Sirenix.Utilities;
using Zenject;

public class GameProjectContext : MonoInstaller
{
    public override void InstallBindings()
    {
        ReflectionUtils.GetAllDerivedTypes<ILocalData>().ForEach(type =>
        {
            var data = Activator.CreateInstance(type);
            this.Container.Bind(type).FromInstance(data).AsCached();
        });
        GameFoundationInstaller.Install(this.Container);
    }
}
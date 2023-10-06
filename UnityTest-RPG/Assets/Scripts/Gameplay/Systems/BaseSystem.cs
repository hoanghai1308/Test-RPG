namespace Gameplay.Systems
{
    using Zenject;

    public abstract class BaseSystem : ISystem, IInitializable, ILateTickable, ITickable
    {
        public void Initialize() { }

        public virtual void Tick()     { }
        public virtual void LateTick() { }
    }
}
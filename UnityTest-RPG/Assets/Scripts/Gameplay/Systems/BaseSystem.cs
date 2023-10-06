namespace Gameplay.Systems
{
    using Zenject;

    public abstract class BaseSystem : ISystem, IInitializable, ILateTickable, ITickable
    {
        public virtual void Initialize() { }

        public virtual void Tick()     { }
        public virtual void LateTick() { }
    }
}
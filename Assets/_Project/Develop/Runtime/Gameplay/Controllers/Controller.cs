using System;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public abstract class Controller : IDisposable
    {
        // Runtime
        private bool _isEnabled = default;

        public virtual void Enable()
            => _isEnabled = true;


        public void UpdateTick(float deltaTime)
        {
            if (!_isEnabled)
                return;

            UpdateLogic(deltaTime);
        }

        public void Dispose()
            => Disable();

        public virtual void Disable()
            => _isEnabled = false;

        protected abstract void UpdateLogic(float deltaTime);
    }
}
using System;
using System.Collections.Generic;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class ControllersUpdateService
    {
        // References
        private readonly List<ControllerToRemoveReason> _controllers = null;

        public ControllersUpdateService()
            => _controllers = new List<ControllerToRemoveReason>();

        public void Add(Controller controller, Func<bool> removeReason)
            => _controllers.Add(new ControllerToRemoveReason(controller, removeReason));

        public void UpdateTick(float deltaTime)
        {
            _controllers.RemoveAll(item => item.RemoveReason.Invoke());

            foreach (ControllerToRemoveReason item in _controllers)
                item.Controller.UpdateTick(deltaTime);
        }

        private class ControllerToRemoveReason
        {
            public Controller Controller { get; } = null;
            public Func<bool> RemoveReason { get; } = null;

            public ControllerToRemoveReason(Controller controller, Func<bool> removeReason)
            {
                Controller = controller;
                RemoveReason = removeReason;
            }
        }
    }
}
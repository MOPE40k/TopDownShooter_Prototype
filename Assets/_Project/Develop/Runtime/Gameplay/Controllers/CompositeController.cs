using System.Collections.Generic;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class CompositeController : Controller
    {
        // References
        private readonly IEnumerable<Controller> _controllers = null;

        public CompositeController(Controller[] controllers)
        {
            if (controllers.Length == 0)
                throw new System.ArgumentOutOfRangeException($"{nameof(controllers)} is EMPTY!");

            _controllers = controllers;
        }

        public override void Enable()
        {
            base.Enable();

            foreach (Controller controller in _controllers)
                controller.Enable();
        }

        public override void Disable()
        {
            base.Disable();

            foreach (Controller controller in _controllers)
                controller.Disable();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            foreach (Controller controller in _controllers)
                controller.UpdateTick(deltaTime);
        }
    }
}
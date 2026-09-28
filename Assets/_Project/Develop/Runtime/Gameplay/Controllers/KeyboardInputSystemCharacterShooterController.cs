using UnityEngine.InputSystem;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class KeyboardInputSystemCharacterShooterController : Controller
    {
        // References
        private InputSystemActions _inputActions = default;
        private readonly IShootable _shootable = default;

        public KeyboardInputSystemCharacterShooterController(
            InputSystemActions inputActions,
            IShootable shootable)
        {
            _inputActions = inputActions;
            _shootable = shootable;
        }

        public override void Enable()
        {
            base.Enable();

            _inputActions.Player.Attack.performed += OnShootPerformed;
        }

        public override void Disable()
        {
            base.Disable();

            _inputActions.Player.Attack.performed -= OnShootPerformed;
        }

        protected override void UpdateLogic(float _)
        { }

        private void OnShootPerformed(InputAction.CallbackContext _)
            => _shootable?.Shoot();
    }
}
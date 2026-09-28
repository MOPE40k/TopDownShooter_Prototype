using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class KeyboardInputSystemCharacterMovableController : Controller
    {
        // References
        private InputSystemActions _inputActions = default;
        private readonly IDirectionalMovable _movable = default;

        public KeyboardInputSystemCharacterMovableController(
            InputSystemActions inputActions,
            IDirectionalMovable movable)
        {
            _inputActions = inputActions;
            _movable = movable;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            var input = _inputActions.Player.Move.ReadValue<Vector2>();

            var moveDirection = new Vector3(input.x, 0f, input.y);

            _movable?.SetMoveDirection(moveDirection);
        }
    }
}
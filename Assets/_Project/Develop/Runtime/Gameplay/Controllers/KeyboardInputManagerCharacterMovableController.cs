using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class KeyboardInputManagerCharacterMovableController : Controller
    {
        // Consts
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        // References
        private readonly IDirectionalMovable _movable = null;

        public KeyboardInputManagerCharacterMovableController(IDirectionalMovable movable)
            => _movable = movable;

        protected override void UpdateLogic(float deltaTime)
        {
            Vector3 input = new Vector3(
                Input.GetAxisRaw(HorizontalAxisName),
                0f,
                Input.GetAxisRaw(VerticalAxisName));

            _movable?.SetMoveDirection(input);
        }
    }
}
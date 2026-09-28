using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class AlongDirectionRotatableController : Controller
    {
        // References
        private readonly IMovable _movable = null;
        private readonly IDirectionalRotatable _rotatable = null;

        public AlongDirectionRotatableController(IMovable movable, IDirectionalRotatable rotatable)
        {
            _movable = movable;
            _rotatable = rotatable;
        }

        protected override void UpdateLogic(float deltaTime)
            => _rotatable.SetRotateDirection(_movable.CurrentVelocity);
    }
}
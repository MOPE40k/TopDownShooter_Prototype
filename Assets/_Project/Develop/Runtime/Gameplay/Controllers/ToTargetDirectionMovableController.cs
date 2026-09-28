using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class ToTargetDirectionMovableController : Controller
    {
        // References
        private readonly IDirectionalMovable _movable = null;
        private readonly Transform _target = null;

        public ToTargetDirectionMovableController(IDirectionalMovable movable, Transform target)
        {
            _movable = movable;
            _target = target;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_target == null)
                return;

            Vector3 direction = _target.position - _movable.CurrentPosition;

            _movable.SetMoveDirection(direction);
        }
    }
}
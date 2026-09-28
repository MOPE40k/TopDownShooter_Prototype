using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class RandomDirectionMovableController : Controller
    {
        // References
        private readonly IDirectionalMovable _movable = null;

        // Settings
        private readonly float _changeDirectionInterval = 0f;

        // Runtime
        private float _time = 0f;

        public RandomDirectionMovableController(IDirectionalMovable movable, float changeDirectionInterval)
        {
            _movable = movable;
            _changeDirectionInterval = changeDirectionInterval;
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_time >= _changeDirectionInterval)
            {
                _movable.SetMoveDirection(new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)));
                _time -= _changeDirectionInterval;
            }

            _time += deltaTime;
        }
    }
}
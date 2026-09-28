using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement.RigidbodyMovement
{
    public class VelocityRigidbodyDirectionalMover : DirectionalMover
    {
        // References
        private readonly Rigidbody _rigidbody = null;

        public VelocityRigidbodyDirectionalMover(Rigidbody rigidbody, float speed) : base(speed)
        {
            _rigidbody = rigidbody;
            _rigidbody.isKinematic = false;
        }

        public override void UpdateTick(float deltaTime)
            => _rigidbody.velocity = CurrentVelocity;
    }
}
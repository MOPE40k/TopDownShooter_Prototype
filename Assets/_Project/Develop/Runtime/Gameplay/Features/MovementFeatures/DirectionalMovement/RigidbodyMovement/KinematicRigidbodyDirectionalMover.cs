using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement.RigidbodyMovement
{
    public class KinematicRigidbodyDirectionalMover : DirectionalMover
    {
        // References
        private readonly Rigidbody _rigidbody = null;

        public KinematicRigidbodyDirectionalMover(Rigidbody rigidbody, float speed) : base(speed)
        {
            _rigidbody = rigidbody;
            _rigidbody.isKinematic = true;
        }

        public override void UpdateTick(float deltaTime)
            => _rigidbody.MovePosition(_rigidbody.position + CurrentDirection.normalized * Speed * deltaTime);
    }
}
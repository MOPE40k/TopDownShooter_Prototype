using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation.RigidbodyRotation
{
    public class KinematicRigidbodyDirectionalRotator : DirectionalRotator
    {
        // References
        private readonly Rigidbody _rigidbody = null;

        public KinematicRigidbodyDirectionalRotator(Rigidbody rigidbody, float speed) : base(speed)
            => _rigidbody = rigidbody;

        public override Quaternion CurrentRotation => _rigidbody.rotation;

        protected override void ApplyRotation(Quaternion rotation)
            => _rigidbody.MoveRotation(rotation);
    }
}
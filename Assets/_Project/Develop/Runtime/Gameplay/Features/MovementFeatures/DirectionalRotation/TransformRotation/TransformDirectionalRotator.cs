using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation.TransformRotation
{
    public class TransformDirectionalRotator : DirectionalRotator
    {
        // References
        private readonly Transform _transform = null;

        public TransformDirectionalRotator(Transform transform, float speed) : base(speed)
            => _transform = transform;

        // Runtime
        public override Quaternion CurrentRotation => _transform.rotation;

        protected override void ApplyRotation(Quaternion rotation)
            => _transform.rotation = rotation;
    }
}
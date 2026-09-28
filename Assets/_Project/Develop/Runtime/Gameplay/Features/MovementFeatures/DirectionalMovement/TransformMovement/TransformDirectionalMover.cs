using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement.TransformMovement
{
    public class TransformDirectionalMover : DirectionalMover
    {
        // References
        private readonly Transform _transform = null;

        public TransformDirectionalMover(Transform transform, float speed) : base(speed)
            => _transform = transform;

        public override void UpdateTick(float deltaTime)
            => _transform.position += CurrentDirection.normalized * Speed * deltaTime;
    }
}
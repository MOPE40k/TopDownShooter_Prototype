using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement
{
    public abstract class DirectionalMover
    {
        // Settings
        protected readonly float Speed = 0f;

        // Runtime
        protected Vector3 CurrentDirection = Vector3.zero;

        protected DirectionalMover(float speed)
            => Speed = speed;

        // Runtime
        public Vector3 CurrentVelocity => CurrentDirection.normalized * Speed;

        public void SetDirection(Vector3 direction)
            => CurrentDirection = direction;

        public abstract void UpdateTick(float deltaTime);
    }
}
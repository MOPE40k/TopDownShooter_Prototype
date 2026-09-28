using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation
{
    public abstract class DirectionalRotator
    {
        // Settings
        private readonly float _speed = 0f;

        protected DirectionalRotator(float speed)
            => _speed = speed;

        // Runtime
        public abstract Quaternion CurrentRotation { get; }

        protected Vector3 CurrentDirection = Vector3.zero;

        public void SetDirection(Vector3 direction)
            => CurrentDirection = direction;

        public void UpdateTick(float deltaTime)
        {
            if (CurrentDirection.sqrMagnitude < 0.05f * 0.05f)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(CurrentDirection);

            float rotationDelta = _speed * deltaTime;

            ApplyRotation(Quaternion.RotateTowards(CurrentRotation, targetRotation, rotationDelta));
        }

        protected abstract void ApplyRotation(Quaternion rotation);
    }
}
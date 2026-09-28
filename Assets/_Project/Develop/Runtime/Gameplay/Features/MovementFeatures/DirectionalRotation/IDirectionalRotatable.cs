using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation
{
    public interface IDirectionalRotatable
    {
        // Runtime
        Quaternion CurrentRotation { get; }

        void SetRotateDirection(Vector3 direction);
    }
}
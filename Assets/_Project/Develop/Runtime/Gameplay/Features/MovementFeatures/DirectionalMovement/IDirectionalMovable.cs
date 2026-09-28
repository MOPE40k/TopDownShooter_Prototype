using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement
{
    public interface IDirectionalMovable : IMovable, ITransformPosition
    {
        void SetMoveDirection(Vector3 direction);
    }
}
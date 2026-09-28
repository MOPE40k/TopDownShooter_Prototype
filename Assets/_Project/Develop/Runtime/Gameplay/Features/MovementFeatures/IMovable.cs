using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures
{
    public interface IMovable
    {
        Vector3 CurrentVelocity { get; }
    }
}
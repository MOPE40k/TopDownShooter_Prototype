using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures
{
    public interface ITransformPosition
    {
        Vector3 CurrentPosition { get; }
    }
}
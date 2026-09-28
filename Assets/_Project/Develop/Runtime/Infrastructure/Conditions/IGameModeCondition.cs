using System;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions
{
    public interface IGameModeCondition
    {
        // Delegates
        event Action Completed;

        void UpdateTick(float deltaTime);
    }
}
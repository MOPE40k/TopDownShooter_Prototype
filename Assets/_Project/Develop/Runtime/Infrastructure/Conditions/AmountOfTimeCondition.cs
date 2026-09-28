using System;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions
{
    public class AmountOfTimeCondition : IGameModeCondition
    {
        // Delegates
        public event Action Completed;

        // References
        private readonly LevelConfig _levelConfig = null;

        // Runtime
        private float _currentTime = 0f;

        public AmountOfTimeCondition(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;

            _currentTime = 0f;
        }

        public void UpdateTick(float deltaTime)
        {
            _currentTime += deltaTime;

            if (_currentTime >= _levelConfig.TimeToWin)
                Completed?.Invoke();
        }
    }
}
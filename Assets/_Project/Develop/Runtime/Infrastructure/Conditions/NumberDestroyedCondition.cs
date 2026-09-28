using System;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions
{
    public class NumberDestroyedCondition : IGameModeCondition
    {
        // Delegates
        public event Action Completed;

        // References
        private readonly EnemiesSpawnService _enemiesSpawner = null;
        private readonly LevelConfig _levelConfig = null;

        public NumberDestroyedCondition(EnemiesSpawnService enemiesSpawner, LevelConfig levelConfig)
        {
            _enemiesSpawner = enemiesSpawner;
            _levelConfig = levelConfig;
        }

        public void UpdateTick(float deltaTime)
        {
            if (_enemiesSpawner.Destroyed >= _levelConfig.DestroyedEnemiesCountToWin)
                Completed?.Invoke();
        }
    }
}
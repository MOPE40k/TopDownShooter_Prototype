using System;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Holders.MainHeroHolder;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions
{
    public class CondititionsFactory
    {
        private readonly LevelConfig _levelConfig = null;
        private readonly EnemiesSpawnService _enemiesSpawner = null;
        private readonly IMainHeroProvider _mainHeroProvider = null;

        public CondititionsFactory(
            LevelConfig levelConfig,
            EnemiesSpawnService enemiesSpawner,
            IMainHeroProvider mainHeroProvider)
        {
            _levelConfig = levelConfig;
            _enemiesSpawner = enemiesSpawner;
            _mainHeroProvider = mainHeroProvider;
        }

        public IGameModeCondition GetWinCondition()
            => GetConditionFrom(_levelConfig.WinCondition);

        public IGameModeCondition GetDefeatCondition()
            => GetConditionFrom(_levelConfig.DefeatCondition);

        private IGameModeCondition GetConditionFrom(GameModeConditionTypes conditionType)
            => conditionType switch
            {
                GameModeConditionTypes.ByAmountOfTime
                    => new AmountOfTimeCondition(_levelConfig),

                GameModeConditionTypes.ByNumberDestroyed
                    => new NumberDestroyedCondition(_enemiesSpawner, _levelConfig),

                GameModeConditionTypes.MainHeroIsDead
                    => new MainHeroIsDeadCondition(_mainHeroProvider.MainHero),

                GameModeConditionTypes.NumberOfEnemiesIsTooLarge
                    => new NumberOfEnemiesIsTooLargeCondition(_enemiesSpawner, _levelConfig),

                _
                    => throw new ArgumentOutOfRangeException($"Unknown gamemode condition {conditionType}")
            };
    }
}
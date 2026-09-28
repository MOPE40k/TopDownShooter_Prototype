using System;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Holders.MainHeroHolder;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Random = UnityEngine.Random;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure
{
    public class GameMode : IDisposable
    {
        // Delegates
        public event Action Win = null;
        public event Action Defeat = null;

        // References
        private LevelConfig _levelConfig = null;
        private IMainHeroProvider _mainHeroProvider = null;
        private EnemiesSpawnService _enemiesSpawner = null;

        private CondititionsFactory _conditionsFactory = null;

        // Runtime
        private IGameModeCondition _winCondition = null;
        private IGameModeCondition _defeatCondition = null;
        private bool _isRunning = false;

        public GameMode(
            LevelConfig levelConfig,
            CondititionsFactory condititionsFactory,
            IMainHeroProvider mainHeroProvider,
            EnemiesSpawnService enemiesSpawner)
        {
            _levelConfig = levelConfig;
            _conditionsFactory = condititionsFactory;
            _mainHeroProvider = mainHeroProvider;
            _enemiesSpawner = enemiesSpawner;
        }

        public void Start()
        {
            _winCondition = _conditionsFactory.GetWinCondition();
            _winCondition.Completed += OnWinConditionCompleted;

            _defeatCondition = _conditionsFactory.GetDefeatCondition();
            _defeatCondition.Completed += OnDefeatConditionCompleted;

            _mainHeroProvider.MainHero.Destroyed += OnMainHeroDestroyed;

            _enemiesSpawner.StartSpawn(
                _levelConfig.EnemiesSpawnPoints,
                _levelConfig.EnemyConfigs[Random.Range(0, _levelConfig.EnemyConfigs.Length)],
                _mainHeroProvider.MainHero.transform,
                _levelConfig.EnemiesRadiusSpawn,
                _levelConfig.EnemiesCount);

            _isRunning = true;
        }

        public void UpdateTick(float deltaTime)
        {
            if (!_isRunning)
                return;

            _winCondition.UpdateTick(deltaTime);

            _defeatCondition.UpdateTick(deltaTime);
        }

        private void OnMainHeroDestroyed(MonoDestroyable _)
            => OnDefeatConditionCompleted();

        private void OnWinConditionCompleted()
        {
            ProcessEndGame();

            Win?.Invoke();
        }

        private void OnDefeatConditionCompleted()
        {
            ProcessEndGame();

            Defeat?.Invoke();
        }

        private void ProcessEndGame()
        {
            _isRunning = false;

            _enemiesSpawner.StopSpawn();
            _enemiesSpawner.Clear();

            // _winCondition.Completed -= OnWinConditionCompleted;
            // _defeatCondition.Completed -= OnDefeatConditionCompleted;

            // _mainHeroProvider.MainHero.Destroyed -= OnMainHeroDestroyed;
            Dispose();
        }

        public void Dispose()
        {
            _winCondition.Completed -= OnWinConditionCompleted;
            _defeatCondition.Completed -= OnDefeatConditionCompleted;

            _mainHeroProvider.MainHero.Destroyed -= OnMainHeroDestroyed;
        }
    }
}
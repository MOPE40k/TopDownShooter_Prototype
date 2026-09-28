using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils.Reactive;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature
{
    public class EnemiesSpawnService
    {
        // Consts
        private const int FalseSpawnCount = 1000;

        // References
        private EnemiesFactory _enemiesFactory = null;

        private MonoBehaviour _coroutineRunner = null;

        // Settings
        private float _spawnInterval = 0f;

        // Runtime
        private int _currentFalseSpawnCount = 0;
        private Coroutine _spawnRoutine = null;
        private ReactiveList<EnemyCharacter> _enemies = null;

        public EnemiesSpawnService(EnemiesFactory enemiesFactory, MonoBehaviour coroutineRunner, float spawnInterval)
        {
            _enemiesFactory = enemiesFactory;
            _coroutineRunner = coroutineRunner;
            _spawnInterval = spawnInterval;

            _enemies = new ReactiveList<EnemyCharacter>();
        }

        // Runtime
        public int Destroyed { get; private set; } = 0;
        public int LivedEnemiesCount => _enemies.Count;

        public void StartSpawn(
            IReadOnlyList<Vector3> spawnPoints,
            EnemyConfig config,
            Transform target,
            float radius,
            int count)
        {
            if (_spawnRoutine != null)
                StopSpawn();

            _spawnRoutine = _coroutineRunner.StartCoroutine(SpawnRoutine(spawnPoints, config, target, radius, count));
        }

        public void StopSpawn()
            => _coroutineRunner.StopCoroutine(_spawnRoutine);

        public void Clear()
        {
            foreach (EnemyCharacter enemy in _enemies.Items)
                enemy.MonoDestroy();

            _enemies.Clear();
        }

        public IEnumerator SpawnRoutine(
            IReadOnlyList<Vector3> spawnPoints,
            EnemyConfig config,
            Transform target,
            float radius,
            int count)
        {
            Destroyed = 0;

            Vector3 spawnPoint = Vector3.zero;

            for (int i = 0; i < count; i++)
            {
                _currentFalseSpawnCount = 0;

                yield return new WaitForSeconds(_spawnInterval);

                do
                {
                    _currentFalseSpawnCount++;

                    if (_currentFalseSpawnCount >= FalseSpawnCount)
                        throw new System.TimeoutException($"{this.GetType().Name} Too many spawn attempts!");

                    var randomPositionInCircle = Random.insideUnitCircle * radius;
                    var offset = new Vector3(randomPositionInCircle.x, 0f, randomPositionInCircle.y);

                    var randomSpawnPointIndex = Random.Range(0, spawnPoints.Count);
                    spawnPoint = spawnPoints[randomSpawnPointIndex] + offset;
                }
                while (Physics.Raycast(spawnPoint + Vector3.up, Vector3.down) == false);

                EnemyCharacter enemyCharacter = GetRandomEnemy(config, spawnPoint, target);

                enemyCharacter.Destroyed += OnEnemyDestroyed;

                _enemies.Add(enemyCharacter);
            }
        }

        private EnemyCharacter GetRandomEnemy(EnemyConfig config, Vector3 spawnPosition, Transform target)
        {
            if (Random.value < 0.5f)
                return _enemiesFactory.GetToTargerMoveEnemy(
                    config,
                    spawnPosition,
                    target);
            else
                return _enemiesFactory.GetRandomDirectionMoveEnemy(
                    config,
                    spawnPosition);
        }

        private void OnEnemyDestroyed(MonoDestroyable destroyable)
        {
            Destroyed++;

            destroyable.Destroyed -= OnEnemyDestroyed;

            _enemies.Remove((EnemyCharacter)destroyable);
        }
    }
}
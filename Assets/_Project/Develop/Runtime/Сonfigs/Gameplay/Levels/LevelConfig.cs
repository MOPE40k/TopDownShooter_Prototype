using System.Collections.Generic;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Infrastructure.Conditions;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Create LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        // Consts
        private const string StartHeroPositionTag = "StartHeroPosition";
        private const string SpawnerPositionTag = "SpawnerPosition";

        [field: Header("Conditions Settings:")]
        [field: SerializeField] public GameModeConditionTypes WinCondition { get; private set; } = default;
        [field: SerializeField] public GameModeConditionTypes DefeatCondition { get; private set; } = default;
        [field: SerializeField] public float TimeToWin { get; private set; } = 60f;
        [field: SerializeField] public int DestroyedEnemiesCountToWin { get; private set; } = 10;
        [field: SerializeField] public int EnemiesCountToDefeat { get; private set; } = 10;

        [field: Space]
        [field: Header("Main Hero Settings:")]
        [field: SerializeField] public Vector3 MainHeroSpawnPoint { get; private set; } = default;

        [field: Space]
        [field: Header("Enemies Settings:")]
        [field: SerializeField] public EnemyConfig[] EnemyConfigs { get; private set; } = default;
        [SerializeField] private List<Vector3> _enemiesSpawnPoints = default;
        [field: SerializeField, Min(2f)] public float EnemiesRadiusSpawn { get; private set; } = 10f;
        [field: SerializeField, Min(1)] public int EnemiesCount { get; private set; } = 3;
        [field: SerializeField, Min(0.1f)] public float EnemiesSpawnInterval { get; private set; } = 5f;

        [field: Space]
        [field: Header("Scene Settings:")]
        [field: SerializeField] public string EnviromentSceneName { get; private set; } = default;

        // Runtime
        public IReadOnlyList<Vector3> EnemiesSpawnPoints => _enemiesSpawnPoints;

        [ContextMenu("UpdateHeroStartPosition")]
        private void UpdateHeroStartPosition()
        {
            GameObject startPositionObject = GameObject.FindGameObjectWithTag(StartHeroPositionTag);

            if (startPositionObject != null)
                MainHeroSpawnPoint = startPositionObject.transform.position;
        }

        [ContextMenu("UpdateSpawnersPositions")]
        private void UpdateSpawnersPositions()
        {
            GameObject[] spawners = GameObject.FindGameObjectsWithTag(SpawnerPositionTag);

            if (spawners.Length == 0)
                throw new System.IndexOutOfRangeException("Spawners not found!");

            _enemiesSpawnPoints.Clear();

            foreach (GameObject spawner in spawners)
                _enemiesSpawnPoints.Add(spawner.transform.position);
        }
    }
}
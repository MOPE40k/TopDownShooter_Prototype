using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Create LevelsListConfig", fileName = "LevelsListConfig")]

    public class LevelsListConfig : ScriptableObject
    {
        [field: SerializeField] public LevelConfig[] LevelConfigs { get; private set; } = default;

        public LevelConfig GetRandom()
            => LevelConfigs[Random.Range(0, LevelConfigs.Length)];
    }
}
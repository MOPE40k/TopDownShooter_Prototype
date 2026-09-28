using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters
{
    public class CharacterConfigBase : ScriptableObject
    {
        [field: Header("Settings:")]
        [field: SerializeField, Min(0.1f)] public float MoveSpeed { get; private set; } = 10f;
        [field: SerializeField, Min(0.1f)] public float RotationSpeed { get; private set; } = 900f;
        [field: SerializeField, Min(0.1f)] public float MaxHealth { get; private set; } = 100f;
        [field: SerializeField, Min(0.1f)] public float TimeToSpawn { get; private set; } = 1f;
    }
}
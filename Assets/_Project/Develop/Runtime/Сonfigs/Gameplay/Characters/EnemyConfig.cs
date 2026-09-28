using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Create EnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : CharacterConfigBase
    {
        [field: Header("Enemy Settings:")]
        [field: SerializeField] public EnemyCharacter Prefab { get; private set; } = default;
        [field: SerializeField, Min(0.1f)] public float Damage { get; private set; } = 10f;
        [field: SerializeField, Min(0.1f)] public float ChangeMoveDirectionInterval { get; private set; } = 1f;
    }
}
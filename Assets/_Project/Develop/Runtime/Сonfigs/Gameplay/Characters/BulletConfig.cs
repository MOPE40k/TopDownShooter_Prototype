using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Create BulletConfig", fileName = "BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        [field: Header("References:")]
        [field: SerializeField] public Bullet Prefab { get; private set; } = default;

        [field: Space]
        [field: Header("Settings:")]
        [field: SerializeField, Min(0.1f)] public float Speed { get; private set; } = 50f;
        [field: SerializeField, Min(0.1f)] public float Damage { get; private set; } = 25f;
        [field: SerializeField, Min(0f)] public float SelfDestroyAfter { get; private set; } = 5f;
    }
}
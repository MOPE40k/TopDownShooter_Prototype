using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Create MainHeroConfig", fileName = "MainHeroConfig")]
    public class MainHeroConfig : CharacterConfigBase
    {
        [field: Header("MainHero Settings:")]
        [field: SerializeField] public MainHeroCharacter CharacterPrefab { get; private set; } = default;
    }
}
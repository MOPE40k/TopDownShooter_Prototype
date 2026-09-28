using Cinemachine;
using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.CameraFeatures;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters
{
    public class MainHeroFactory
    {
        // References
        private readonly CharactersFactory _charactersFactory = null;
        private readonly CamerasFactory _camerasFactory = null;
        private readonly ControllersFactory _controllersFactory = null;
        private readonly ControllersUpdateService _controllersUpdateService = null;

        private Controller _controller = default;

        public MainHeroFactory(
            CharactersFactory charactersFactory,
            CamerasFactory camerasFactory,
            ControllersFactory controllersFactory,
            ControllersUpdateService controllersUpdateService)
        {
            _charactersFactory = charactersFactory;
            _camerasFactory = camerasFactory;
            _controllersFactory = controllersFactory;
            _controllersUpdateService = controllersUpdateService;
        }

        public MainHeroCharacter GetMainHero(
            MainHeroConfig config,
            ProjectilesFactory projectilesFactory,
            BulletConfig projectileConfig,
            Vector3 spawnPosition)
        {
            MainHeroCharacter character = _charactersFactory.GetMainHeroCharacter(
                config.CharacterPrefab,
                spawnPosition,
                config.MoveSpeed,
                config.RotationSpeed,
                config.MaxHealth,
                config.TimeToSpawn);

            var shooter = new Shooter(projectilesFactory, projectileConfig);
            character.IndividualInit(shooter);

            character.Destroyed += ReleaseCharacter;

            _camerasFactory.GetFollowCameraWithBoundsFor(character);

            ControllerSetup(character);

            return character;
        }

        public void ReleaseCharacter(MonoDestroyable mainHero)
        {
            mainHero.Destroyed -= ReleaseCharacter;

            _controllersFactory.Release(_controller);
        }

        private void ControllerSetup(MainHeroCharacter character)
        {
            _controller = _controllersFactory.GetKeyboardInputSystemMoveShootController(
                character,
                character,
                character);

            _controller.Enable();

            _controllersUpdateService.Add(_controller, () => character.IsDestroyed);
        }
    }
}
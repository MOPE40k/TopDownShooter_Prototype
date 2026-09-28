using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters
{
    public class EnemiesFactory
    {
        // References
        private readonly CharactersFactory _charactersFactory = null;
        private readonly ControllersFactory _controllersFactory = null;
        private readonly ControllersUpdateService _controllersUpdateService = null;

        public EnemiesFactory(
            CharactersFactory charactersFactory,
            ControllersFactory controllersFactory,
            ControllersUpdateService controllersUpdateService)
        {
            _charactersFactory = charactersFactory;
            _controllersFactory = controllersFactory;
            _controllersUpdateService = controllersUpdateService;
        }

        public EnemyCharacter GetRandomDirectionMoveEnemy(
            EnemyConfig config,
            Vector3 spawnPosition)
        {
            EnemyCharacter character = _charactersFactory.GetEnemyCharacter(
                config.Prefab,
                spawnPosition,
                config.MoveSpeed,
                config.RotationSpeed,
                config.MaxHealth,
                config.TimeToSpawn);

            character.IndividualInit(config.Damage);

            Controller controller = _controllersFactory.GetRandomDirectionMoveRotateController(
                character,
                character,
                config.ChangeMoveDirectionInterval);

            controller.Enable();

            _controllersUpdateService.Add(controller, () => character.IsDestroyed);

            return character;
        }

        public EnemyCharacter GetToTargerMoveEnemy(
            EnemyConfig config,
            Vector3 spawnPosition,
            Transform target)
        {
            EnemyCharacter instance = _charactersFactory.GetEnemyCharacter(
                config.Prefab,
                spawnPosition,
                config.MoveSpeed,
                config.RotationSpeed,
                config.MaxHealth,
                config.TimeToSpawn);

            instance.IndividualInit(config.Damage);

            Controller controller = _controllersFactory.GetToTargetDirectionMoveRotateController(
                instance,
                instance,
                target);

            controller.Enable();

            _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

            return instance;
        }
    }
}
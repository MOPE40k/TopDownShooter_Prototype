using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Entities;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement.RigidbodyMovement;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation.RigidbodyRotation;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation.TransformRotation;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement.TransformMovement;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters
{
    public class CharactersFactory
    {
        private Character GetCharacter(
            Character prefab,
            Vector3 spawnPosition,
            float moveSpeed,
            float rotationSpeed,
            float maxHealth,
            float timeToSpawn,
            Quaternion? spawnRotation = null,
            Transform parent = null)
        {
            Character instance = GameObject.Instantiate(
                prefab,
                spawnPosition,
                spawnRotation ?? Quaternion.identity,
                parent);

            DirectionalMover mover = null;
            DirectionalRotator rotator = null;

            if (instance.TryGetComponent(out Rigidbody rigidbody))
            {
                mover = new VelocityRigidbodyDirectionalMover(rigidbody, moveSpeed);
                rotator = new KinematicRigidbodyDirectionalRotator(rigidbody, rotationSpeed);
            }
            else
            {
                mover = new TransformDirectionalMover(instance.transform, moveSpeed);
                rotator = new TransformDirectionalRotator(instance.transform, rotationSpeed);
            }

            HealthService health = new HealthService(maxHealth);

            CoroutineTimer spawnTimer = new CoroutineTimer(instance);

            instance.CommonInit(mover, rotator, health, spawnTimer, timeToSpawn);

            return instance;
        }

        public MainHeroCharacter GetMainHeroCharacter(
            Character prefab,
            Vector3 spawnPosition,
            float moveSpeed,
            float rotationSpeed,
            float maxHealth,
            float timeToSpawn,
            Quaternion? spawnRotation = null,
            Transform parent = null)
        {

            return GetCharacter(
                prefab,
                spawnPosition,
                moveSpeed,
                rotationSpeed,
                maxHealth,
                timeToSpawn,
                spawnRotation,
                parent) as MainHeroCharacter;
        }

        public EnemyCharacter GetEnemyCharacter(
            Character prefab,
            Vector3 spawnPosition,
            float moveSpeed,
            float rotationSpeed,
            float maxHealth,
            float timeToSpawn,
            Quaternion? spawnRotation = null,
            Transform parent = null)
        {
            return GetCharacter(
                prefab,
                spawnPosition,
                moveSpeed,
                rotationSpeed,
                maxHealth,
                timeToSpawn,
                spawnRotation,
                parent) as EnemyCharacter;
        }
    }
}
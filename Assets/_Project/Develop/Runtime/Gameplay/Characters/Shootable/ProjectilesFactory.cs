using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement.RigidbodyMovement;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable
{
    public class ProjectilesFactory
    {
        public Bullet GetBullet(
            Bullet prefab,
            Vector3 spawnPosition,
            float speed,
            float damage,
            float selfDestroyAfter = 0f,
            Quaternion? initialRotation = null,
            Transform parent = null)
        {
            var instance = GameObject.Instantiate(
                prefab,
                spawnPosition,
                initialRotation ?? Quaternion.identity,
                parent);

            var mover = new VelocityRigidbodyDirectionalMover(
                instance.GetComponent<Rigidbody>(),
                speed);

            instance.Init(mover, damage, selfDestroyAfter);

            return instance;
        }
    }
}
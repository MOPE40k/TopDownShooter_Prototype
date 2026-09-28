using TopDownShooterDemo.Assets._Project.Develop.Runtime.Configs.Gameplay.Characters;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable
{
    public class Shooter
    {
        // References
        private readonly ProjectilesFactory _factory = null;
        private readonly BulletConfig _config = null;

        public Shooter(ProjectilesFactory factory, BulletConfig config)
        {
            _factory = factory;
            _config = config;
        }

        public void Shoot(Vector3 spawnPosition, Vector3 direction)
        {
            Bullet bullet = _factory.GetBullet(
                _config.Prefab,
                spawnPosition,
                _config.Speed,
                _config.Damage,
                _config.SelfDestroyAfter);

            bullet.SetMoveDirection(direction);
        }
    }
}
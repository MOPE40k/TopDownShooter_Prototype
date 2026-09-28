using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters
{
    public class MainHeroCharacter : Character, IShootable
    {
        [Header("References:")]
        [SerializeField] private Transform _shootPoint = null;
        [SerializeField] private Transform _cameraTarget = null;

        // References
        private Shooter _shooter = null;
        public Transform CameraTarget => _cameraTarget;

        public void IndividualInit(Shooter shooter)
            => _shooter = shooter;

        public void Shoot()
            => _shooter.Shoot(_shootPoint.position, _shootPoint.forward);
    }
}
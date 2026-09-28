using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers
{
    public class KeyboardInputManagerShooterController : Controller
    {
        // Consts
        private const KeyCode ShootKey = KeyCode.Space;

        // References
        private readonly IShootable _shootable = null;

        public KeyboardInputManagerShooterController(IShootable shootable)
            => _shootable = shootable;

        protected override void UpdateLogic(float deltaTime)
        {
            if (Input.GetKeyDown(ShootKey))
                _shootable?.Shoot();
        }
    }
}
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Controllers;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures
{
    public class AiRandomDirectionMovement : MonoBehaviour
    {
        [Header("References:")]
        [SerializeField] private EnemyCharacter _character = null;

        [Space]
        [Header("Settings:")]
        [SerializeField] private float _changeDirectionInterval = 5f;

        // References
        private Controller _randomDirectionMovableController = null;

        private void Awake()
        {
            _randomDirectionMovableController = new CompositeController(new Controller[]
            {
                new RandomDirectionMovableController(_character, _changeDirectionInterval),
                new AlongDirectionRotatableController(_character, _character)
            });
        }

        private void OnEnable()
            => _randomDirectionMovableController.Enable();

        private void OnDisable()
            => _randomDirectionMovableController.Disable();

        private void Update()
            => _randomDirectionMovableController.UpdateTick(Time.deltaTime);
    }
}
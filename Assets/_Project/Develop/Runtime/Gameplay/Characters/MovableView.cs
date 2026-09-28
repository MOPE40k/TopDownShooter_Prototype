using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters
{
    public class MovableView : MonoBehaviour, IInitializable
    {
        // Consts
        private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
        private const float VelocitySqrForRunning = 0.05f * 0.05f;

        [Header("References:")]
        [SerializeField] private Animator _animator = null;

        // References
        private IMovable _movable = null;

        // Runtime
        private bool _isInit = false;

        public void Init()
        {
            _movable = GetComponentInParent<IMovable>();

            _isInit = true;
        }

        private void Update()
        {
            if (_isInit == false)
                return;

            if (CanRunning())
                StartRunning();
            else
                StopRunning();
        }

        private void StartRunning()
            => _animator.SetBool(IsRunningKey, true);

        private void StopRunning()
            => _animator.SetBool(IsRunningKey, false);

        private bool CanRunning()
            => _movable.CurrentVelocity.sqrMagnitude > VelocitySqrForRunning;
    }
}
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Entities;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalRotation;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.SpawnFeature;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters
{
    public abstract class Character : MonoDestroyable, IDirectionalMovable, IDirectionalRotatable, IDamageable, ICanSpawn
    {
        // References
        private DirectionalMover _mover = null;
        private DirectionalRotator _rotator = null;
        private HealthService _health = null;
        private CoroutineTimer _spawnTimer = null;

        // Settings
        private float _timeToSpawn = 0f;

        // Runtime
        public bool IsDead { get; private set; } = false;
        public IHealthService Health => _health;
        public Vector3 CurrentVelocity => _mover.CurrentVelocity;
        public Quaternion CurrentRotation => _rotator.CurrentRotation;
        public float TimeToSpawn => _spawnTimer.TimeLimit;
        public Vector3 CurrentPosition => transform.position;

        public void CommonInit(
            DirectionalMover mover,
            DirectionalRotator rotator,
            HealthService health,
            CoroutineTimer spawnTimer,
            float timeToSpawn)
        {
            IsDead = false;

            _mover = mover;
            _rotator = rotator;

            _health = health;
            _health.Current.Changed += OnHealthChange;

            _spawnTimer = spawnTimer;

            _timeToSpawn = timeToSpawn;

            _spawnTimer.StartTimer(_timeToSpawn);

            foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
                initializable.Init();
        }

        private void FixedUpdate()
        {
            if (IsDead)
                return;

            float fDt = Time.fixedDeltaTime;

            _mover.UpdateTick(fDt);
            _rotator.UpdateTick(fDt);
        }

        private void OnDestroy()
        {
            _health.Current.Changed -= OnHealthChange;

            MonoDestroy();

            _spawnTimer?.Dispose();
        }

        public void SetMoveDirection(Vector3 direction)
            => _mover.SetDirection(direction);

        public void SetRotateDirection(Vector3 direction)
            => _rotator.SetDirection(direction);

        public void TakeDamage(float damageValue)
            => _health.Reduce(damageValue);

        public bool InSpawnProcess(out float elapsedTime)
            => _spawnTimer.InProcess(out elapsedTime);

        private void OnHealthChange(float _, float newValue)
        {
            if (newValue == 0f)
            {
                IsDead = true;

                MonoDestroy();
            }
        }
    }
}
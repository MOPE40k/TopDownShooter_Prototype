using UnityEngine;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Entities;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeatures.DirectionalMovement;
using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Shootable
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoDestroyable, IDirectionalMovable
    {
        // References
        private DirectionalMover _mover = default;

        // Settings
        private float _damage = 25f;
        private float _selfDestroyAfter = 5f;

        // Runtime
        public Vector3 CurrentVelocity => _mover.CurrentVelocity;
        public Vector3 CurrentPosition => transform.position;

        public void Init(DirectionalMover mover, float damage, float selfDestroyAfter)
        {
            _mover = mover;

            _damage = damage;

            _selfDestroyAfter = selfDestroyAfter;
        }

        public void SetMoveDirection(Vector3 direction)
        {
            _mover.SetDirection(direction);
            _mover.UpdateTick(Time.deltaTime);

            MonoDestroy(_selfDestroyAfter);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);

            MonoDestroy();
        }
    }
}
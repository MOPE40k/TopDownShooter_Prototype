using TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Entities;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters
{
    public class EnemyCharacter : Character
    {
        // Settings
        private float _damage = 0f;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
                damageable.TakeDamage(_damage);
        }

        public void IndividualInit(float damage)
            => _damage = damage;
    }
}
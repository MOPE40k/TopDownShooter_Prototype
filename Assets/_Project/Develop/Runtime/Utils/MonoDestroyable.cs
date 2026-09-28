using System;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils
{
    public class MonoDestroyable : MonoBehaviour
    {
        // Delegates
        public event Action<MonoDestroyable> Destroyed = default;

        // Runtime
        public bool IsDestroyed { get; private set; } = default;

        public void MonoDestroy(float destroyAfter = 0f)
        {
            Destroy(gameObject, destroyAfter);

            IsDestroyed = true;

            Destroyed?.Invoke(this);
        }
    }
}
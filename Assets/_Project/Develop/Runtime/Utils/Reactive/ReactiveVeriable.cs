using System;
using UnityEngine;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils.Reactive
{
    public class ReactiveVeriable<T> : IReadOnlyReactiveVeriable<T> where T : IEquatable<T>
    {
        // Delegates
        public event Action<T, T> Changed = null;

        // Runtime
        private T _value = default(T);

        public ReactiveVeriable()
            => _value = default(T);

        public ReactiveVeriable(T value)
            => _value = value;

        // Runtime
        public T Value
        {
            get => _value;
            set
            {
                T oldValue = _value;

                _value = value;

                if (_value.Equals(oldValue) == false)
                    Changed?.Invoke(oldValue, value);
            }
        }
    }
}
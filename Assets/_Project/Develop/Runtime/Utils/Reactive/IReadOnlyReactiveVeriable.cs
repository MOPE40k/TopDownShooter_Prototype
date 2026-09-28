using System;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils.Reactive
{
    public interface IReadOnlyReactiveVeriable<T>
    {
        // Delegates
        event Action<T, T> Changed;

        // Runtime
        T Value { get; }
    }
}
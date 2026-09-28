using TopDownShooterDemo.Assets._Project.Develop.Runtime.Utils.Reactive;

namespace TopDownShooterDemo.Assets._Project.Develop.Runtime.Gameplay.Characters.Entities
{
    public interface IHealthService
    {
        public IReadOnlyReactiveVeriable<float> Max { get; }
        public IReadOnlyReactiveVeriable<float> Current { get; }
    }
}